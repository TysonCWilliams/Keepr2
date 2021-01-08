<template>
  <div class="vaultDetailsPage container-fluid justify-content-center">
    <h1>{{ vault.title }}</h1>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'VaultDetailsPage',
  setup(props) {
    const route = useRoute()
    onMounted(async() => {
      // console.log(route.params)
      await vaultsService.getVaultDetails(route.params.vaultId)
    })
    return {
      vaultDetails: computed(() => AppState.vaultDetails)
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
