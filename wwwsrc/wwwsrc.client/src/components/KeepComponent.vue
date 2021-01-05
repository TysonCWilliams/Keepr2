<template>
  <div>

    <div class="card" style="width: 18rem;">
      <img :src="keep.img" class="card-img-top" alt="...">
      <div class="card-body">
      </div>
      <button class="btn" @click="toggleModal()">
        {{keep.title}}
      </button>
    </div>

    <div style="display: block !important;" v-if="state.showModal" class="modal fade show" aria-hidden="false">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">{{keep.title}}</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            {{ keep.body }}
            <img :src="keep.img" style="position: relative; width: 100%" alt="">
          </div>
          <div class="modal-footer">
            <button @click="toggleModal()" type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary">Save changes</button>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script>

import { computed, reactive } from 'vue'
// import { AppState } from '../AppState'
import router from '../router'
export default {
  name: 'KeepComponent',
  props: ['keepProp'],
  setup(props) {
    const state = reactive({
      showModal: false
    })
    return {
      state,
      keep: computed(() => props.keepProp),
      navigateTo(route) {
        console.log(route)
        router.push(route)
      },
      toggleModal() {
        state.showModal = !state.showModal
        console.log('Modal: ' + state.showModal)
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
  .card {
    height: auto;
    width: auto;
    position: relative;
  }
</style>
