import { logger } from '../utils/Logger'
import { api } from './AxiosService'
// import { AppState } from '../AppState'

class VaultsService {
  async getPublicVaults() {
    try {
      const res = await api.get('api/vaults')
      logger.log(res.data)
      return res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async createVault(newVault) {
    try {
      await api.post('api/vaults', newVault)
      this.getPublicVaults()
    } catch (error) {
      logger.log(error)
    }
  }

  async createVaultKeep(newVaultKeep) {
    try {
      const res = await api.post('api/vaultkeeps', newVaultKeep)
      // this.getPublicVaults()
      console.log(res)
      return res
    } catch (error) {
      logger.log(error)
    }
  }

  async getVaultKeeps(vaultId) {
    try {
      const res = await api.get('api/vaultkeeps/' + vaultId + '/')
      // this.getPublicVaults()
      console.log(res)
      return res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getAllVaultsForUser(id) {
    try {
      const res = await api(`api/profiles/${id}/vaults`)
      return res.data
      // NOTE dont forget to add your js doc types in the appstate
      // AppState.vaults = res.data.map(e => new Vault(e.title, e.body))
      // this now has intellisense on 'p' AppState.vaults.forEach(p=> p.)
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultDetails(vaultId) {
    try {
      const res = await api.get('api/vaults/' + vaultId)
      console.log(res)
      return res.data
    } catch (err) {
      logger.error(err)
    }
  }
}

export const vaultsService = new VaultsService()
