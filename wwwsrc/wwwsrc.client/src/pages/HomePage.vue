<template>
  <div class="home-page container-fluid bg-light">
    <div class="grid">
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

.grid {
    margin-top: 2vh;
    column-count: 1;
  }
  @media (min-width: 700px) {
    .grid {
      column-count: 2;
    }
  }
  @media (min-width: 975px) {
    .grid {
      column-count: 3;
    }
  }
  @media (min-width: 1300px) {
    .grid {
      column-count: 4;
    }
  }
  @media (min-width: 1485px) {
    .grid {
      column-count: 5;
    }
  }

  .home {
    text-align: center;
    user-select: none;
  }

  // img {
  //   height: 150px;
  //   width: 150px;
  // }
</style>
