<template>
  <div class="home">
    <!-- keeps -->
    <div class="ml-2 mt-3"
         style="flex-direction: column; width: 100vw; position: relative;  column-count: 4;
  column-gap: 10px;"
    >
      <div style="  display: grid;
  grid-template-rows: 1fr auto; width: fit-content;  grid-row: 1 / -1;
  grid-column: 1; break-inside: avoid;"
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

  img {
    height: 150px;
    width: 150px;
  }
</style>
