<template>
  <div class="creatorPage text-center container-fluid">
    {{ keep.creator }}
    <keep-component v-for="keep in keeps" :keep-prop="keep" :key="keep.id"></keep-component>
  </div>
</template>

<script>
  import { computed, onMounted, reactive } from 'vue'
  import { AppState } from '../AppState'
  import { keepsService } from '../services/KeepsService'
  // import { profilesService } from '../services/ProfilesService'
  import { KeepComponent } from '../components/KeepComponent.vue'
  // import { vaultsService } from '../services/VaultsService'
  export default {
    name: 'CreatorPage',
    setup() {
      onMounted(async () => {
        // if (!AppState.profile.id) {
        //   await profilesService.getProfile()
        // }
        await keepsService.getMyKeeps()
        console.log(AppState.keeps)
      })
      const state = reactive({

      })
      return {
        state,
        profile: computed(() => AppState.profile),
        keeps: computed(() => AppState.keeps),
        vaults: computed(() => AppState.vaults),

        components: { KeepComponent }
      }
    }
  }
</script>

<style scoped>
  img {
    max-width: 100px;
  }
</style>
