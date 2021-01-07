<template>
  <div class="home">
    <!-- keeps -->
    <div class="ml-2 mt-3" style="flex-direction: column;">
      <keep-component
        v-for="keep in keeps"
        :keep-prop="keep"
        :key="keep.id"
      ></keep-component>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import { KeepComponent } from '../components/KeepComponent.vue'

export default {
  name: 'Home',
  setup() {
    onMounted(() => {
      keepsService.getPublicKeeps()
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
