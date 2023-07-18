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
  // keeps and vaults
  async getAccountVaults(){ 
    try {
      
      const vres = await api.get('/account/vaults')
      // logger.log('[VRES DATA]', vres.data)
      AppState.myVaults = vres.data.map((v) => new Vault(v));
      logger.log(AppState.myVaults)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async getAccountKeeps(){ 
    try {
      const kres = await api.get(`/api/profiles/${AppState.account.id}/keeps`)
      AppState.myKeeps = kres.data.map((k) => new Keep(k));
      logger.log(AppState.myKeeps)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
