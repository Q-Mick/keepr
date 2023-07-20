import { api } from "./AxiosService";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js"
import { VaultKeep } from "../models/VaultKeep.js";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop.js";

class VaultKeepsService {
    async createVaultKeep(vaultKeepData) {
        const res = await api.post('api/vaultkeeps', vaultKeepData)
        const newVaultKeep = new VaultKeep(res.data)
        logger.log(newVaultKeep)
        
     
        return newVaultKeep
      }
}
export const vaultKeepsService = new VaultKeepsService()