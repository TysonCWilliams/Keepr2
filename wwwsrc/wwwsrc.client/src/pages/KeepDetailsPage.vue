<template>
  <div class="keepDetails container-fluid justify-content-center">
    <div class="card mb-3" style="max-width: 540px;">
      <div class="row no-gutters">
        <div class="col-md-4">
          <img :src="keepDetails.img" class="card-img" alt="...">
        </div>
        <div class="col-md-8">
          <div class="card-body">
            <h5 class="card-title">
              Keep Title: {{ keepDetails.title }}
            </h5>
            <p class="card-text">
              {{ keepDetails.body }}
            </p>
            <p class="card-text">
              <small class="text-muted">Last updated 3 mins ago</small>
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'KeepDetails',
  setup(props) {
    const route = useRoute()
    onMounted(async() => {
      // console.log(route.params)
      await keepsService.getKeepDetails(route.params.keepId)
    })
    return {
      keepDetails: computed(() => AppState.keepDetails)
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
