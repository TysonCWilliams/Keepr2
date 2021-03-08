<template>
  <div class="row">
    <div style="height: 100vw;" class="ml-2">
      <div style="break-inside: avoid;"
           class="card-body"
           v-for="keep in keeps"
           :key="keep.id"
      >
        <keep-component

          :keep-prop="keep"
          :key="keep.id"
        ></keep-component>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { KeepComponent } from '../components/KeepComponent.vue'

export default {
  name: 'Home',
  setup() {
    onMounted(() => {
      keepsService.getPublicKeeps()
      vaultsService.getPublicVaults()
    })
    return {
      keeps: computed(() => AppState.keeps),
      components: {
        KeepComponent
      }
    }
  }
}
</script>

<style scoped lang="scss">
  .home {
    text-align: center;
    user-select: none;
  }

  // img {
  //   height: 150px;
  //   width: 150px;
  // }
</style>
