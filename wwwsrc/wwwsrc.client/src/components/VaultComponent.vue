<template>
  <div class="card mt-3" style="width: 18rem; border-width: thick; border-color: chartreuse;">
    <img src="https://i.pinimg.com/originals/09/bd/5b/09bd5b04107d4cc82fadeb7624bfcdb7.jpg" class="card-img-top" alt="">
    <div class="card-body">
      <p class="card-text">
        {{ vaultProp.name }}
      </p>
    </div>
    <button class="btn" style="" @click="navigateTo('/vaultDetails/' + vaultProp.id)">
      {{ vaultProp.name }}
    </button>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import router from '../router'

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
      },

      navigateTo(route) {
        router.push(route)
      }
    }
  },
  components: {}
}
</script>
