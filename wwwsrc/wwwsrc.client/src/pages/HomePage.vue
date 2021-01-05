<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <img
      :src="image"
      alt="CodeWorks Logo"
    >
    <h1 class="my-5 bg-dark text-light p-3 rounded d-flex align-items-center">
      <span class="mx-2 text-white">Vue 3 Starter</span>
    </h1>
    <!-- keeps -->
    <div style="flex-direction:column">
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
