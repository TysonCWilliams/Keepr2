<template>
  <div v-if="state.vaultDetails" class="vaultDetailsPage container-fluid justify-content-center">
    <h1>{{ state.vaultDetails.name }}</h1>
    <h4 class="mt-1 info-vk">
      Keeps: {{ state.vaultKeeps.length }}
    </h4>
    <keep-component v-for="keep in state.vaultKeeps" :keep-prop="keep" :key="keep.id"></keep-component>
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
