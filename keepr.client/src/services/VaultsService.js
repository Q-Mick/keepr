import { api } from "./AxiosService";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop.js";

class VaultsService {
  async getVault() {
    const res = await api.get("/api/vaults");
    AppState.vaults = res.data.map((v) => new Vault(v));
    logger.log("[Vault]", AppState.vaults)
  }
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`);
    let foundVault = new Vault(res.data)
    logger.log(`[vault FROM GET BY ID] ---->`,foundVault)
    AppState.vaultKeeps = await this.getKeepsInVault(vaultId)
    return foundVault
  }
  async getKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`);
    AppState.vaultKeeps = res.data.map((vk) => new Keep (vk))
    logger.log(`[VAULT KEEPS FOUND] ---->`, AppState.vaultKeeps)
    
  }
  async setActiveVault(vaultId) {
    AppState.actVault = await this.getVaultById(vaultId)
    logger.log(`[ACTIVE Vault SET] - ID ${AppState.actVault.id} - Name:${AppState.actVault.name}`)
    
    
  }
  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    const newVault = new Vault(res.data)
    logger.log(newVault)
    AppState.vaults.unshift(newVault)
    Pop.success(`Vault created!`)
    return newVault
  }

  async editVault(vaultId, vaultData){
    const res = await api.put(`api/vaults/${vaultId}`, vaultData)
    return res.data
  }
  async deleteVault(vaultId){
    const res = await api.delete(`api/vaults/${vaultId}`)
    logger.log('[vault DELETED ]', res.data)
    const vaultToDelete = AppState.myVaults.findIndex(v => v.id == vaultId)
    AppState.myVaults.splice(vaultToDelete, 1)
  }
  
}

export const vaultsService = new VaultsService();
