import { api } from "./AxiosService";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js"
import { VaultKeep } from "../models/VaultKeep.js";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop.js";

class VaultKeepsService {
    async createVaultKeep(vaultKeepData) {
      if (AppState.user.id.length == 0){
        Pop.error("Log in to start adding keeps to your vaults")
        return
      }
        Pop.toast("Keep added to your vault")
        const res = await api.post('api/vaultkeeps', vaultKeepData)
        const newVaultKeep = new VaultKeep(res.data)
        logger.log(newVaultKeep)
        return newVaultKeep
      }
      async deleteKeepFromVault(vaultKeepId){
        const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        logger.log('[REMOVED KEEP FROM VAULT]', res.data)
        const vaultKeepToDelete = AppState.vaultKeeps.findIndex(vk => vk.id == vaultKeepId)
        AppState.vaultKeeps.splice(vaultKeepToDelete, 1)
      }
}
export const vaultKeepsService = new VaultKeepsService()