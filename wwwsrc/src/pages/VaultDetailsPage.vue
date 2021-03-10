<template>
  <div v-if="state.vaultDetails" class="vaultDetailsPage container-fluid">
    <h1 style="color: red;">
      {{ state.vaultDetails.name }}
    </h1>
    <h3 style="color: blue;">
      {{ state.vaultDetails.description }}
    </h3>
    <h4 class="mt-2 mb-2 info-vk">
      Total Keeps: {{ state.vaultKeeps.length }}
    </h4>
    <div style="display: flex; flex-direction: row; flex-wrap: wrap; margin-top: 30px;">
      <keep-component style="margin-left: 15px; margin-right: 15px; margin-top: 20px; margin-bottom: 20px; position: relative; width: 100%; height: 100%" v-for="keep in state.vaultKeeps" :keep-prop="keep" :key="keep.id"></keep-component>
    </div>
  </div>
</template>

<script>
import { onMounted, reactive } from 'vue'
import { useRoute } from 'vue-router'
// import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { profilesService } from '../services/ProfilesService'
export default {
  name: 'VaultDetailsPage',
  setup(props) {
    const route = useRoute()
    const state = reactive({
      vaultDetails: null,
      vaultKeeps: [],
      userProfile: null
    })
    onMounted(() => {
      setTimeout(() => {
        vaultsService.getVaultDetails(route.params.vaultId).then((res) => {
          state.vaultDetails = res
          // console.log(res)
          profilesService.getProfileById(res.creatorId).then(res => {
            state.userProfile = res
            vaultsService.getVaultKeeps(route.params.vaultId).then((res) => {
              res.forEach((item, index) => {
                item.creator = state.userProfile
              })
              state.vaultKeeps = res
            })
          })
        })
      }, 2000)
    })
    return {
      state
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
