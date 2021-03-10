<template>
  <div class="profile text-center container-fluid">
    <div class="row">
      <div class="col-sm-1 col-lg-12 mt-3 ml-1">
        <img class="rounded" :src="profile.picture" alt="">
      </div>
      <div class="col-sm-1 col-lg-12 mt-4">
        <h2 class="profile-title mr-1">
          {{ profile.name }}
        </h2>
        <h4 class="mt-1 info-vk" style="width: 140px;">
          Vaults:  {{ state.myVaults.length }}
        </h4>
        <h4 class="mt-1 info-vk" style="width: 140px;">
          Keeps:  {{ state.myKeeps.length }}
        </h4>
      </div>
    </div>
    <div class="row ml-2 mt-4">
      <h3 class="mt-2">
        Vaults:
      </h3>
      <button type="button"
              style="display: block;"
              @click="toggleVaultModal()"
              class="btn btn-light ml-3 mb-3"
              data-toggle="modal"
              data-target="#form"
      >
        <i class="fas fa-plus-square fa-2x fa-spin"></i>
      </button>
    </div>
    <div class="col-sm-12" style="display: flex; flex-direction: row; flex-wrap: wrap;">
      <div class="ml-2 mr-3 mt-3" v-for="vault in state.myVaults" :key="vault.id">
        <vault-component style="position: relative; max-width: 100%;" :vault-prop="vault"></vault-component>
      </div>
    </div>

    <div style="display: block !important;"
         v-if="state.showVaultModal"
         class="modal fade show"
         id="form"
         tabindex="-1"
         role="dialog"
    >
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header border-bottom-0">
            <h5 class="modal-title" id="exampleModalLabel">
              Create Vault
            </h5>
            <button @click="toggleVaultModal()" type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form>
            <div class="modal-body">
              <div class="form-group">
                <label for="vault-name">Name</label>
                <input type="text"
                       v-model="state.newVault.name"
                       class="form-control"
                       id="vault-name"
                       aria-describedby="vault-name"
                       placeholder="Enter Vault Name..."
                >
              </div>
              <div class="form-group">
                <label for="vault-description">Description</label>
                <input type="text"
                       v-model="state.newVault.description"
                       class="form-control"
                       id="vault-description"
                       placeholder="Description"
                >
              </div>
              <div class="form-check">
                <input type="checkbox" class="form-check-input" id="isPrivate" v-model="state.newVault.isPrivate">
                <label class="form-check-label" for="isPrivate">Private?</label>
              </div>
            </div>
            <div class="modal-footer border-top-0 d-flex justify-content-center">
              <button type="submit" @click="createVault()" class="btn btn-success">
                Create
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
    <div class="row ml-2 mt-4">
      <h4 class="mt-2">
        Keeps
      </h4>
      <button type="button"
              style="display: block;"
              @click="toggleKeepModal()"
              class="btn btn-light ml-3 mb-3"
              data-toggle="modal"
              data-target="#form"
      >
        <i class="fas fa-plus-square fa-2x fa-spin"></i>
      </button>
    </div>

    <div style="display: block !important;"
         v-if="state.showKeepModal"
         class="modal fade show"
         id="form"
         tabindex="-1"
         role="dialog"
         aria-labelledby="exampleModalLabel"
         aria-hidden="false"
    >
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header border-bottom-0">
            <h5 class="modal-title" id="exampleModalLabel">
              Create Keep
            </h5>
            <button @click="toggleKeepModal()" type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <form>
            <div class="modal-body">
              <div class="form-group">
                <label for="keep-name">Title</label>
                <input type="text"
                       v-model="state.newKeep.name"
                       class="form-control"
                       id="keep-name"
                       aria-describedby="keep-name"
                       placeholder="Enter Keep Name..."
                >
              </div>
              <div class="form-group">
                <label for="keep-image">Image</label>
                <input type="text"
                       v-model="state.newKeep.img"
                       class="form-control"
                       id="keep-image"
                       placeholder="URL..."
                >
              </div>
              <div class="form-group">
                <label for="keep-description">Description</label>
                <input type="text"
                       v-model="state.newKeep.description"
                       class="form-control"
                       id="keep-description"
                       placeholder="Enter Description..."
                >
              </div>
            </div>
            <div class="modal-footer border-top-0 d-flex justify-content-center">
              <button type="submit" @click="createKeep()" class="btn btn-success">
                Create
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>

    <div class="col-sm-12" style="display: flex; flex-direction: row; flex-wrap: wrap;">
      <div class="ml-2 mr-3 mt-3" v-for="keep in state.myKeeps" :key="keep.id">
        <keep-component style="position: relative; width: 100%;" :keep-prop="keep"></keep-component>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'
import { KeepComponent } from '../components/KeepComponent.vue'
import { vaultsService } from '../services/VaultsService'
import { VaultComponent } from '../components/VaultComponent.vue'

export default {
  name: 'Profile',
  setup() {
    onMounted(async() => {
      if (!AppState.profile.id) {
        await profilesService.getProfile()
      }
      const myKeeps = await keepsService.getMyKeeps()
      state.myKeeps = myKeeps
      const myVaults = await vaultsService.getAllVaultsForUser(AppState.profile.id)
      // console.log(myVaults)
      state.myVaults = myVaults
    })
    const state = reactive({
      myKeeps: [],
      myVaults: [],
      showKeepModal: false,

      showVaultModel: false,

      newKeep: {
        name: '',
        description: '',
        img: ''
      },
      newVault: {
        name: '',
        description: '',
        isPrivate: false
      }
    })
    return {
      state,
      createKeep() {
        // state.newKeep
        keepsService.createKeep(state.newKeep)
      },
      createVault() {
        vaultsService.createVault(state.newVault)
      },
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),

      toggleKeepModal() {
        state.showKeepModal = !state.showKeepModal
        // console.log('Modal: ' + state.showModal)
      },
      toggleVaultModal() {
        state.showVaultModal = !state.showVaultModal
        // console.log('Modal: ' + state.showVaultModal)
      }

    }
  },
  components: { KeepComponent, VaultComponent }
}
</script>

<style scoped>

.grid {
    /* margin-top: 2vh; */
    line-height: 40px;
    column-count: 1;
  }

  @media (min-width: 700px) {
    .grid {
      column-count: 2;
    }
  }
  @media (min-width: 900px) {
    .grid {
      column-count: 3;
    }
  }
  @media (min-width: 1100px) {
    .grid {
      column-count: 4;
    }
  }
  @media (min-width: 1200px) {
    .grid {
      column-count: 5;
    }
  }

  /* img {
    max-width: 100px;
  } */

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
