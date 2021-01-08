<template>
  <div>
    <div class="container">
      <div class="card mt-3 ml-2" style="width: 18rem;">
        <img :src="keep.img" class="card-img" alt="...">
        <!-- <div class="card-title">
        </div> -->
        <button class="btn" @click="toggleKeepModal()">
          {{ keep.name }}
        </button>
      </div>
    </div>

    <div style="display: block !important;" v-if="state.showKeepModal" class="modal fade show" aria-hidden="false">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              {{ keep.name }}
            </h5>
            <button @click="toggleKeepModal()" type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            {{ keep.description }}
            <img :src="keep.img" style="position: relative; width: 100%" alt="">
          </div>
          <div class="modal-footer">
            <img @click="navigateTo('/users/' + keep.creator.id)"
                 :src="keep.creator.picture"
                 height="40"
                 class="rounded"
                 alt=""
            >
            <button type="button" class="btn btn-outline-info">
              Add to Vault
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- <img :src="user.picture" height="40" class="rounded" alt=""> -->
</template>
<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import router from '../router'
// import { profilesService } from '../services/ProfilesService'

export default {
  name: 'KeepComponent',
  props: ['keepProp'],
  setup(props) {
    const state = reactive({
      showKeepModal: false
    })

    return {
      state,
      user: computed(() => AppState.user),
      profile: computed(() => AppState.profile),
      keep: computed(() => props.keepProp),

      navigateTo(route) {
        // console.log(route)
        router.push(route)
      },
      toggleKeepModal() {
        state.showKeepModal = !state.showKeepModal
        // console.log('Modal: ' + state.showModal)
      }

      // deleteKeep() {
      //   keepService.deleteKeep(props.keepProp._id)
      // },
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
    border: solid;
    /* border-radius: 30px; */
    border-color: rgba(5, 5, 5, 0.938);
    background-color: rgba(15, 15, 15, 0.164);
    opacity: 10;
  }
</style>
