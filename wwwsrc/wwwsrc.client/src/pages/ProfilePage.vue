<template>
  <div class="about text-center">
    <h1>Welcome {{ profile.name }}</h1>
    <img
      class="rounded"
      :src="profile.picture"
      alt=""
    >
    <p>{{ profile.email }}</p>
    <h5>Add a Keep</h5>
    <div class="row justify-content-center">
      <div class="col-6 mt-5">
        <!-- <form class="form group mt-4" @submit.prevent="createKeep"> -->
        <div class="card">
          <h3 class="keep-title">
            Create a Keep
          </h3>
          <div class="card-body">
            <div class="row justify-content-center">
              <div class="form-group mx-sm-3 mb-2">
                <input style="margin-bottom: 1em;" type="text" class="form-control my-border" v-model="state.newKeep.title" placeholder="Title">
              </div>
            </div>
            <div class="row justify-content-center">
              <div class="form-group mx-sm-3 mb-2">
                <input style="margin-bottom: 1em;" type="text" class="form-control my-border" v-model="state.newKeep.img" placeholder="Image...">
              </div>
            </div>
            <div class="row justify-content-center">
              <div class="form-group mx-sm-3 mb-4">
                <textarea style="height: 100px;" class="form-control my-border" v-model="state.newKeep.body" placeholder="Description"></textarea>
              </div>
            </div>
            <div class="row justify-content-center">
              <button @click="createKeep()" class="btn btn-primary mb-2">
                Create
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <keep-component
      v-for="keep in keeps"
      :keep-prop="keep"
      :key="keep.id"
    ></keep-component>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { profileService } from '../services/ProfileService'
import { KeepComponent } from '../components/KeepComponent.vue'
export default {
  name: 'Profile',
  setup() {
    onMounted(async() => {
      if (!AppState.profile.id) {
        await profileService.getProfile()
      }
      await keepsService.getMyKeeps()
      console.log(AppState.keeps)
    })
    const state = reactive({
      newKeep: {
        title: '',
        body: '',
        img: '',
      }
    })
    return {
      state,
      createKeep() {
        // state.newKeep
        keepsService.createKeep(state.newKeep)
      },
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      components: {
        KeepComponent
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}

.card{
  border-color: aquamarine;
  border-style: solid;
  border-width: thick;
  background-color: rgba(0, 0, 0, 0.651);
}

.my-border{
  border-color: rgb(45, 255, 3);
  border-style: solid;
  border-width: thick;
}
</style>
