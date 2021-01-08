<template>
  <div class="card mt-3" style="width: 18rem; border-width: thick; border-color: chartreuse;">
    <img src="https://cdn.hipwallpaper.com/i/1/55/zDTHLN.jpg" class="card-img-top" alt="">
    <div class="card-body">
      <p class="card-text">
        {{ vaultProp.title }}
      </p>
    </div>
    <!-- <button class="btn" style="" @click="navigateTo('/vaultDetails/' + vaultd)"></button> -->
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'VaultComponent',
  props: ['vaultProp'],
  setup(props) {
    const state = reactive({

    })
    return {
      state,
      user: computed(() => AppState.user),
      profile: computed(() => AppState.profile),
      vault: computed(() => props.vaultProp),

      deleteVault() {
        vaultsService.deleteVault(props.vaultProp._id)
      }
    }
  },
  components: {}
}
</script>
