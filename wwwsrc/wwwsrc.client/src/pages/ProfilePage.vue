<template>
  <div class="profilePage text-center">
    <h1>Welcome {{ profile.name }}</h1>
    <img
      class="rounded"
      :src="profile.picture"
      alt=""
    >
    <p>{{ profile.email }}</p>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
export default {
  name: 'ProfilePage',
  setup() {
    onMounted(async() => {
      if (!AppState.profile.id) {
        await profileService.getProfile()
      }
    })
    return {
      profile: computed(() => AppState.profile),
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
