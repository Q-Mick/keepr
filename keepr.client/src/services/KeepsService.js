import { api } from "./AxiosService";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop.js";

class KeepsService {
  async getKeeps() {
    const res = await api.get("/api/keeps");
    AppState.keeps = res.data.map((k) => new Keep(k));
    logger.log("[Keep]", AppState.keeps)
  }
  async getKeepById(keepId) {
    const res = await api.get(`api/keeps/${keepId}`);

    return new Keep(res.data)
  }

  async setActiveKeep(keepId) {
    AppState.activeKeep = await this.getKeepById(keepId)
    logger.log(`[SETTING ACTIVE KEEP] - ID ${keepId} - Name:${AppState.activeKeep.name}`)
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
}

export const keepsService = new KeepsService();
