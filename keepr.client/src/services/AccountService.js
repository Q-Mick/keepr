import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { Vault } from "../models/Vault.js"
import { Keep } from "../models/Keep.js"
import { Profile } from "../models/Account.js"

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async editAccount(accountData) {
    const res = await api.put("/account", accountData);
    logger.log(res.data)
    AppState.account = new Account(res.data);
  }
  // keeps and vaults
  async getAccountVaults(){ 
    try {
      
      const res = await api.get('/account/vaults')
      // logger.log('[VRES DATA]', vres.data)
      AppState.myVaults = res.data.map((v) => new Vault(v));
      logger.log(AppState.myVaults)
    } catch (err) {
      logger.error('Are you logged in?', err)
    }
  }
  async getAccountKeeps(){ 
    try {
      const res = await api.get(`/api/profiles/${AppState.account.id}/keeps`)
      AppState.myKeeps = res.data.map((k) => new Keep(k));
      logger.log(AppState.myKeeps)
    } catch (err) {
      logger.error('Are you logged in?', err)
    }
  }
  async getProfile(profileId){ 
    try {
      const res = await api.get(`/api/profiles/${profileId}`)
      AppState.activeProfile = new Profile(res.data);
      logger.log(`[PROFILE SET]`, AppState.activeProfile)
    } catch (err) {
      logger.error('Cant find this profile.', err)
    }
  }
  async getProfileKeeps(profileId){ 
    try {
      const res = await api.get(`/api/profiles/${profileId}/keeps`)
      AppState.profileKeeps = res.data.map((k) => new Keep(k));
      logger.log(`[PROFILE KEEPS]`, AppState.profileKeeps)
    } catch (err) {
      logger.error('Cant find this profile.', err)
    }
  }
  async getProfileVaults(profileId){ 
    try {
      const res = await api.get(`/api/profiles/${profileId}/vaults`)
      AppState.profileVaults = res.data.map((v) => new Vault(v));
      logger.log(`[PROFILE VAULTS]`, AppState.profileVaults)
    } catch (err) {
      logger.error('Cant find this profile.', err)
    }
  }
}

export const accountService = new AccountService()
