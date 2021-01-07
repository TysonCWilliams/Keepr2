<!-- <template>
    <div class="container">
      <div class="card" style="width: 18rem;">
        <img src="..." class="card-img-top" alt="...">
        <div class="card-body">
          <p class="card-text">{{ vault.title }}</p>
        </div>
      </div>
    </div>
</template>
<script>

import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import router from '../router'
export default {
  name: 'VaultComponent',
  props: ['vaultProp'],
  setup(props) {
    const state = reactive({

    })
    return {
      state,
      user: computed(() => AppState.user),
      vault: computed(() => props.vaultProp),

      // navigateTo(route) {
      //   console.log(route)
      //   router.push(route)
      // },

      deleteVault() {
        vaultService.deleteVault(props.vaultProp._id)
      },
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
</style> -->
