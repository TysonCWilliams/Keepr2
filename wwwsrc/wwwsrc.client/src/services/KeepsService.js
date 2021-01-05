import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { AppState } from '../AppState'

class KeepsService {
  async getPublicKeeps() {
    try {
      const res = await api.get('api/keeps')
      logger.log(res.data)
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async createKeep(newKeep) {
    try {
      await api.post('api/keeps', newKeep)
      this.getMyKeeps()
    } catch (error) {
      logger.log(error)
    }
  }

  async getMyKeeps() {
    try {
      logger.log(AppState.profile)
      const res = await api(`profile/${AppState.profile.id}/keeps`)
      AppState.keeps = res.data
      // NOTE dont forget to add your js doc types in the appstate
      // AppState.keeps = res.data.map(e => new Keep(e.name, e.description))
      // this now has intellisense on 'p' AppState.keeps.forEach(p=> p.)
    } catch (error) {
      logger.error(error)
    }
  }

  async getKeepDetails(keepId) {
    try {
      const res = await api.get('api/keeps/' + keepId)
      AppState.keepDetails = res.data
    } catch (err) {
      logger.error(err)
    }
  }
}

export const keepsService = new KeepsService()
