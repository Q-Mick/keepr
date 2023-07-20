import { api } from "./AxiosService";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger";
import { Account } from "../models/Account.js";
import Pop from "../utils/Pop.js";

class KeepsService {
  async getKeeps() {
    const res = await api.get("/api/keeps");
    AppState.keeps = res.data.map((k) => new Keep(k));
    logger.log("[Keep]", AppState.keeps)
  }
  async getKeepById(keepId) {
    const res = await api.get(`api/keeps/${keepId}`);
    let foundKeep = new Keep(res.data)
    AppState.actKeep = foundKeep
    logger.log(`[keep FROM GETBYID] ---->`,foundKeep)
    return foundKeep
  }

  async setActiveKeep(keepId) {
    AppState.actKeep = await this.getKeepById(keepId)
    logger.log(`[ACTIVE KEEP SET] - ID ${AppState.actKeep.id} - Name:${AppState.actKeep.name}`)
  }
  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)
    const newKeep = new Keep(res.data)
    logger.log(newKeep)
    AppState.keeps.unshift(newKeep)
    Pop.success(`Keep created!`)
    return newKeep
  }

  async editKeep(keepId, keepData){
    const res = await api.put(`api/keeps/${keepId}`, keepData)
    return res.data
  }
  async deleteKeep(keepId){
    const res = await api.delete(`api/keeps/${keepId}`)
    logger.log('[KEEP DELETED ]', res.data)
    const keepToDelete = AppState.myKeeps.findIndex(v => v.id == keepId)
    AppState.keeps.splice(keepToDelete, 1)
    AppState.myKeeps.splice(keepToDelete, 1)
    this.getKeeps()
    return res.data
  }
  }
export const keepsService = new KeepsService();
