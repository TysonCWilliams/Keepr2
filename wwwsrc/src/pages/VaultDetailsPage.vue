<template>
  <div v-if="state.vaultDetails" class="vaultDetailsPage container-fluid custom-background">
    <h1 style="color: #bbff00; text-decoration: underline; text-shadow: 10px 10px 8px #000000;">
      {{ state.vaultDetails.name }} Vault
    </h1>
    <h3 style="color: silver; font: bold; text-shadow: 10px 10px 8px #000000; font: italic;">
      Description: {{ state.vaultDetails.description }}
    </h3>
    <h4 class="mt-2 mb-2 info-vk" style="color: black; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-weight: bold; font: large;">
      Total Keeps: {{ state.vaultKeeps.length }}
    </h4>
    <div style="display: flex; flex-direction: row; flex-wrap: wrap; margin-top: 30px; margin-bottom: 20px;">
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

.custom-background{
  background-image: url('../assets/img/vault.png');
  background-repeat: no-repeat;
  background-size: cover;
  position: relative;
  height: 100vh;
}

</style>
