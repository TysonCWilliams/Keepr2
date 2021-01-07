<!-- <template>
  <div class="aboutPage text-center container-fluid">
    <div class="row">
      <div class="col-4">
        <h4>Vaults</h4>
          <button type="button" style="display: block;" @click="toggleVaultModal()" class="btn btn-light" data-toggle="modal"
            data-target="#form">
            <i class="fas fa-plus-square fa-2x fa-spin"></i>
          </button>
      </div>
    </div>

    <div style="display: block !important;" v-if="state.showModal" class="modal fade show" id="form" tabindex="-1"
      role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="false">
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header border-bottom-0">
            <h5 class="modal-title" id="exampleModalLabel">Create Vault</h5>
            <button @click="toggleVaultModal()" type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form>
            <div class="modal-body">
              <div class="form-group">
                <label for="vault-title">Title</label>
                <input type="text" v-model="state.newVault.title" class="form-control" id="vault-title"
                  aria-describedby="vault-title" placeholder="Enter Title...">
              </div>
              <div class="form-group">
                <label for="vault-body">Description</label>
                <input type="text" v-model="state.newVault.body" class="form-control" id="vault-body"
                  placeholder="Enter Description...">
              </div>
              <div class="form-check">
                <input type="checkbox" class="form-check-input" id="isPrivate" v-model="state.newVault.isPrivate">
                <label class="form-check-label" for="isPrivate">Private?</label>
              </div>
            </div>
            <div class="modal-footer border-top-0 d-flex justify-content-center">
              <button type="submit" @click="createVault()" class="btn btn-success">Create</button>
            </div>
          </form>
        </div>
      </div>
    </div>
    <vault-component v-for="vault in vaults" :vault-prop="vault" :key="vault.id"></vault-component>
  </div>
</template>

<script>
  import { computed, onMounted, reactive } from 'vue'
  import { AppState } from '../AppState'
  // import { keepsService } from '../services/KeepsService'
  import { profileService } from '../services/ProfileService'
  import { VaultComponent } from '../components/VaultComponent.vue'
  import { vaultsService } from '../services/VaultsService'
  export default {
    name: 'About',
    props: ['vaultProp'],
    setup() {
      onMounted(async () => {
        if (!AppState.profile.id) {
          await profileService.getProfile()
        }
        // await keepsService.getMyKeeps()
        // console.log(AppState.keeps)

        await vaultsService.getPublicVaults()

      })
      const state = reactive({
        showModal: false,

        newKeep: {
          title: '',
          body: '',
          img: '',
        },
        newVault:{
          title: '',
          body: '',
          isPrivate: false,
        }
      })
      return {
        state,
        // createKeep() {
        //   // state.newKeep
        //   keepsService.createKeep(state.newKeep)
        // },
        createVault() {
          vaultsService.createVault(state.newVault)
        },
        profile: computed(() => AppState.profile),
        // keeps: computed(() => AppState.keeps),
        vaults: computed(() => AppState.vaults),

        toggleModal() {
          state.showModal = !state.showModal
          console.log('Modal: ' + state.showModal)
        },

        components: { VaultComponent }
      }
    }
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
    border-color: rgba(43, 255, 0, 0.89);
    border-width: thick;
    color: black;
    font-weight: bolder;
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
    color: crimson;
  }
</style> -->
