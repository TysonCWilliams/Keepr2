<template>
  <div :style="{background: `url(${keep.img}) !important`, backgroundSize: 'cover', borderRadius: '5px', maxWidth: 'fit-content'}">
    <div @click="toggleKeepModal(), incrementViewCount(keep.id)" class="card mt-3" style="width: 16rem; height: 350px">
      <div class="container">
        <!-- <img :src="keep.img" class="card-img" alt="..."> -->
        <!-- <div class="card-title">
        </div> -->
        <div style="position:relative; width: 100%; margin-left: 15px; top: 275px;" class="row">
          <div style="color: white" class="col-6">
            {{ keep.name }}
          </div>
          <button v-if="keep.creator" style="float: right;" class="btn" @click="navigateTo('/users/' + keep.creator.id)">
            <img v-if="keep.creator" class="img rounded" height="50" :src="keep.creator.picture" alt="">
          </button>
        </div>
      </div>
    </div>

    <div style="display: block !important;" v-if="state.showKeepModal" class="modal fade show" aria-hidden="false">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              {{ keep.name }}
            </h5>
            <button @click="toggleKeepModal()" type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            {{ keep.description }}
            <img :src="keep.img" style="position: relative; width: 100%" alt="">
          </div>
          <div class="modal-footer">
            <div class="mr-2">
              Keeps:
              {{ keep.keeps }}
            </div>
            <div class="mr-2">
              Views:
              {{ keep.views }}
            </div>
            <div v-if="keep.creator" class="mr-2">
              {{ keep.creator.name }}
            </div>
            <img v-if="keep.creator"
                 @click="navigateTo('/users/' + keep.creator.id)"
                 :src="keep.creator.picture"
                 height="40"
                 class="rounded"
                 alt=""
            >
            <div class="dropdown">
              <button @click="toggleShowVaultList()" type="button" class="btn btn-outline-info">
                Add to Vault
              </button>
              <div v-if="state.showVaultList" class="dropdown-menu show" aria-labelledby="dropdownMenuButton">
                <button @click="createVaultKeep(keep.id, vault.id), toggleShowVaultList()" class="dropdown-item" v-for="vault in state.vaultOptions" :key="vault.id">
                  {{ vault.name }}
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- <img :src="user.picture" height="40" class="rounded" alt=""> -->
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
            console.log(res)
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
        // console.log(route)
        router.push(route)
      },
      toggleKeepModal() {
        state.showKeepModal = !state.showKeepModal
        // console.log('Modal: ' + state.showModal)
      },
      toggleShowVaultList() {
        state.showVaultList = !state.showVaultList
      },
      createVaultKeep(vaultId, keepId) {
        const newVk = {
          vaultId: vaultId,
          keepId: keepId
        }
        console.log(newVk)
        vaultsService.createVaultKeep(newVk)
      },
      incrementViewCount(id) {
        keepsService.incrementViewCount(id)
      }

      // deleteKeep() {
      //   keepService.deleteKeep(props.keepProp._id)
      // },
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
    border-color: rgba(5, 5, 5, 0.938);
    background: linear-gradient(180deg, transparent, rgba(0,0,0,.75));
    opacity: 10;
  }
</style>
