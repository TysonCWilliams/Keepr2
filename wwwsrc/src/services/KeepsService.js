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

  async incrementViewCount(keepId) {
    try {
      await api.put('api/keeps/' + keepId + '/views')
      this.getMyKeeps()
    } catch (error) {
      logger.log(error)
    }
  }

  async getMyKeeps() {
    try {
      logger.log(AppState.profile)
      const res = await api(`api/profiles/${AppState.profile.id}/keeps`)
      return res.data
      // NOTE dont forget to add your js doc types in the appstate
      // AppState.keeps = res.data.map(e => new Keep(e.title, e.body))
      // this now has intellisense on 'p' AppState.keeps.forEach(p=> p.)
    } catch (error) {
      logger.error(error)
    }
  }

  async getAllKeepsForUser(id) {
    try {
      const res = await api(`api/profiles/${id}/keeps`)
      return res.data
      // NOTE dont forget to add your js doc types in the appstate
      // AppState.keeps = res.data.map(e => new Keep(e.title, e.body))
      // this now has intellisense on 'p' AppState.keeps.forEach(p=> p.)
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteKeep(keepId) {
    try {
      await api.delete('api/keeps/' + keepId)
      this.getMyKeeps()
    } catch (err) {
      logger.error(err)
    }
  }

  async editKeep(keepId, keepData) {
    try {
      await api.put('api/keeps/' + keepId, keepData)
      this.getMyKeeps()
    } catch (err) {
      logger.error(err)
    }
  }
}

export const keepsService = new KeepsService()
