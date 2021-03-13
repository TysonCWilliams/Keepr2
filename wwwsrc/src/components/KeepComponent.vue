<template>
  <div :style="{background: `url(${keep.img}) !important`, backgroundSize: 'cover', borderRadius: '5px', maxWidth: 'fit-content'}">
    <div @click="toggleKeepModal(), incrementViewCount(keep.id)" class="card" style="width: 17rem; height: 350px;">
      <div class="container">
        <div style="position: relative; width: 100%; margin-left: 15px; top: 275px; text-shadow: 10px 10px 8px #000000;" class="row custom-title">
          <div class="mt-3">
            {{ keep.name }}
          </div>
          <button v-if="keep.creator" style="float: right; margin-left: 160px; position: absolute; box-shadow: 10px 10px 8px #000000;" class="btn" @click="navigateTo('/users/' + keep.creator.id)">
            <img v-if="keep.creator" class="img rounded" height="50" :src="keep.creator.picture" alt="">
          </button>
        </div>
      </div>
    </div>

    <div style="display: block !important;" v-if="state.showKeepModal" class="modal fade show" aria-hidden="false">
      <div class="modal-dialog">
        <div class="modal-content" style="border: solid; border-width: thick; border-color: black">
          <div class="modal-header">
            <h5 class="modal-title my-card-title" style="text-shadow: 10px 10px 8px #000000;" id="exampleModalLabel">
              {{ keep.name }}
            </h5>
            <button @click="toggleKeepModal()" type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body custom-desc" style="text-shadow: 10px 10px 8px #000000;">
            {{ keep.description }}
            <img :src="keep.img" class="mt-3" style="position: relative; width: 100%; box-shadow: 10px 10px 8px #000000;" alt="">
          </div>
          <div class="modal-footer">
            <div class="mr-2" style="text-shadow: 10px 10px 8px #000000;">
              Keeps:
              {{ keep.keeps }}
            </div>
            <div class="mr-2" style="text-shadow: 10px 10px 8px #000000;">
              Views:
              {{ keep.views }}
            </div>
            <div v-if="keep.creator" class="mr-2" style="text-shadow: 10px 10px 8px #000000;">
              {{ keep.creator.name }}
            </div>
            <img v-if="keep.creator"
                 @click="navigateTo('/users/' + keep.creator.id)"
                 :src="keep.creator.picture"
                 height="40"
                 class="rounded"
                 style="box-shadow: 10px 10px 8px #000000;"
                 alt=""
            >
            <div class="dropdown">
              <button @click="toggleShowVaultList()" type="button" class="btn btn-outline-info mb-3 mt-3" style="box-shadow: 10px 10px 8px #000000;">
                Add to Vault
              </button>
              <div v-if="state.showVaultList" class="dropdown-menu show" aria-labelledby="dropdownMenuButton">
                <button @click="createVaultKeep(keep, vault), toggleShowVaultList()" class="dropdown-item" style="box-shadow: 10px 10px 8px #000000;" v-for="vault in state.vaultOptions" :key="vault.id">
                  {{ vault.name }}
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import router from '../router'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'

export default {
  name: 'KeepComponent',
  props: ['keepProp'],
  setup(props) {
    const state = reactive({
      showKeepModal: false,
      showVaultList: false,
      vaultOptions: []
    })
    onMounted(() => {
      setTimeout(() => {
        profilesService.getProfile().then(() => {
          vaultsService.getAllVaultsForUser(AppState.profile.id).then(res => {
            res.forEach((item, index) => {
              item.creator = state.userProfile
            })
            state.vaultOptions = res
          })
        })
      }, 4000)
    })
    return {
      state,
      user: computed(() => AppState.user),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.profile),
      keep: computed(() => props.keepProp),

      navigateTo(route) {
        router.push(route)
      },
      toggleKeepModal() {
        state.showKeepModal = !state.showKeepModal
      },
      toggleShowVaultList() {
        state.showVaultList = !state.showVaultList
      },
      createVaultKeep(keep, vault) {
        const newVk = {
          vaultId: vault.id,
          keepId: keep.id
        }
        // console.log(newVk)

        vaultsService.createVaultKeep(newVk)
      },
      incrementViewCount(id) {
        keepsService.incrementViewCount(id)
      }

      // deleteKeep() {
      //   keepsService.deleteKeep(props.keepProp._id)
      // }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
  .card {
    height: auto;
    width: auto;
    position: relative;
    border: solid;
    /* border-radius: 30px; */
    border-color: rgba(0, 0, 0, 0.938);
    box-shadow: 10px 10px 8px #000000;
    background: linear-gradient(180deg, transparent, rgba(0,0,0,.75));
    opacity: 10;
  }

  .custom-title{
    color:#bbff00;
    font-size: large;
    font-weight: bolder;
    font: x-large;
    text-shadow: 0 0 3px #000000, 0 0 5px #2bff00;
    text-decoration: underline;
  }

  .my-card-title{
    color: silver;
    font: xx-large;
    font-weight: bold;
    text-shadow: currentColor;
    font-size: xx-large;
    font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
    text-decoration: underline;
  }

  .custom-desc{
    color: rgb(7, 7, 7);
    font-style: normal;
    font-weight: bolder;
    font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
    text-shadow: currentColor;
  }
</style>
