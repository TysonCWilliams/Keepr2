<template>
  <div v-if="state.userProfile" class="profile text-center container-fluid">
    <div class="row">
      <div class="col-lg-12 mt-3 ml-1 mr-1">
        <img class="rounded" :src="state.userProfile.picture" alt="">
      </div>
      <div class="col-lg-12 mt-3 ml-1">
        <h2 class="profile-title">
          {{ state.userProfile.name }}
        </h2>
      </div>
    </div>

    <div class="row">
      <div class="col-lg-3 mt-3" style="width: 140px;">
        <h4 class="mt-5 info-vk text-center">
          Vaults: {{ state.userVaults.length }}
        </h4>
      </div>
    </div>
    <div class="col-sm-12" style="display: flex; flex-direction: row; flex-wrap: wrap; margin-top: 30px; margin-bottom: 20px;">
      <vault-component style="margin-left: 15px; margin-right: 15px; margin-top: 20px; margin-bottom: 20px; position: relative;" v-for="vault in state.userVaults" :vault-prop="vault" :key="vault.id"></vault-component>
    </div>

    <div class="row">
      <div class="col-lg-3 mt-3" style="width: 140px;">
        <h4 class="mt-4 info-vk text-center">
          Keeps: {{ state.userKeeps.length }}
        </h4>
      </div>
    </div>
      <div class="col-sm-12" style="display: flex; flex-direction: row; flex-wrap: wrap; margin-top: 30px; margin-bottom: 20px;">
      <keep-component style="margin-left: 15px; margin-right: 15px; margin-top: 20px; margin-bottom: 20px; position: relative; width: 100%; height: 100%" v-for="keep in state.userKeeps" :keep-prop="keep" :key="keep.id"></keep-component>
    </div>
  </div>
</template>


<script>
import { onMounted, computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { KeepComponent } from '../components/KeepComponent.vue'
import { VaultComponent } from '../components/VaultComponent.vue'
import { useRoute } from 'vue-router'

export default {
  name: 'UserPage',
  setup() {
    const route = useRoute()
    const state = reactive({
      userProfile: null,
      userKeeps: [],
      userVaults: []
    })
    onMounted(() => {
      setTimeout(() => {
        profilesService.getProfileById(route.params.userId).then(res => {
          state.userProfile = res
          vaultsService.getAllVaultsForUser(route.params.userId).then(res => {
            // console.log(res)
            res.forEach((item, index) => {
              item.creator = state.userProfile
            })
            state.userVaults = res
          })

          keepsService.getAllKeepsForUser(route.params.userId).then(res => {
            // console.log(res)
            res.forEach((item, index) => {
              item.creator = state.userProfile
            })
            state.userKeeps = res
          })
        })
      }, 3000)
    })
    return {
      state,
      keeps: computed(() => AppState.keeps)
    }
  },
  components: { KeepComponent, VaultComponent }
}
</script>

<style scoped>


  img {
    max-width: 100px;
  }

  .card {
    border-color: aquamarine;
    border-style: solid;
    border-width: thick;
    background-color: rgba(0, 0, 0, 0.651);
  }

  button {
    border-style: solid;
    border-color: rgba(0, 255, 234, 0.89);
    border-width: medium;
    color: black;
    font-weight: bold;
  }

  .my-border {
    border-color: rgb(45, 255, 3);
    border-style: solid;
    border-width: thick;
  }

  .profile-title {
    font-family: Arial, Helvetica, sans-serif;
    font-weight: bold;
    font-style: normal;
  }

  .info-vk {
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: normal;
    font-style: normal;
    color: rgb(4, 245, 245);
    background-color: rgba(0, 0, 0, 0.822);
    border-width: thick;
    border-style: solid;
    border-color: rgba(0, 0, 0, 0.877);
  }
</style>
