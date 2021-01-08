<template>
  <div v-if="state.userProfile" class="profile text-center container-fluid">
    <div class="row justify-content-left">
      <div class="col-2 mt-3 ml-3">
        <img class="rounded" :src="state.userProfile.picture" alt="">
      </div>
      <div class="col-2 mt-4">
        <h2 class="profile-title">
          {{ state.userProfile.name }}
        </h2>
        <h4 class="mt-1 info-vk">
          Vaults:
        </h4>
        <h4 class="mt-1 info-vk">
          Keeps:
        </h4>
        <keep-component v-for="keep in keeps" :keep-prop="keep" :key="keep.id"></keep-component>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed, reactive } from 'vue'
import { AppState } from '../AppState'
import router from '../router'
import { profilesService } from '../services/ProfilesService'
import { keepsService } from '../services/KeepsService'
import { KeepComponent } from '../components/KeepComponent.vue'

export default {
  name: 'UserPage',
  setup() {
    // const route = useRoute()
    const state = reactive({
      userProfile: null
    })
    onMounted(async() => {
      const userProfile = await profilesService.getProfileById(router.currentRoute._value.params.userId)
      // eslint-disable-next-line no-unused-expressions
      state.userProfile = userProfile

      await keepsService.getMyKeeps()
    })
    return {
      state,
      keeps: computed(() => AppState.keeps)
    }
  },
  components: { KeepComponent }
}
</script>

<style scoped>
  img {
    max-width: 100px;
  }

  .card {
    border-color: aquamarine;
    border-style: solid;
    border-width: thick;
    background-color: rgba(0, 0, 0, 0.651);
  }

  button {
    border-style: solid;
    border-color: rgba(0, 255, 234, 0.89);
    border-width: medium;
    color: black;
    font-weight: bold;
  }

  .my-border {
    border-color: rgb(45, 255, 3);
    border-style: solid;
    border-width: thick;
  }

  .profile-title {
    font-family: Arial, Helvetica, sans-serif;
    font-weight: bold;
    font-style: normal;
  }

  .info-vk {
    font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    font-weight: normal;
    font-style: normal;
    color: rgb(4, 245, 245);
    background-color: rgba(0, 0, 0, 0.822);
    border-width: thick;
    border-style: solid;
    border-color: rgba(0, 0, 0, 0.877);
  }
</style>
