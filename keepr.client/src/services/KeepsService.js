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
    // logger.log("[Keep]", AppState.keeps)
  }
  async getKeepById(keepId) {
    const res = await api.get(`api/keeps/${keepId}`);
    let foundKeep = new Keep(res.data);
    // logger.log(`[keep FROM GETBYID] ---->`,foundKeep)
    return foundKeep;
  }
  setActiveVaultKeep(keep) {
    AppState.activeVaultKeep = keep;
    logger.log("from service", keep);
  }
  async setActiveKeep(keepId) {
    // TODO if the keep has a vaultKeepId... don't make the req to the service, instead just save that obj directly to appstate active keep
    AppState.actKeep = await this.getKeepById(keepId);
    logger.log(
      `[ACTIVE KEEP VAULTKEEPID] - ID ${AppState.actKeep.vaultKeepId}`
    );
  }
  async createKeep(keepData) {
    const res = await api.post("api/keeps", keepData);
    const newKeep = new Keep(res.data);
    // logger.log(newKeep)
    AppState.keeps.unshift(newKeep);
    Pop.success(`Keep created!`);
    return newKeep;
  }

  async editKeep(keepId, keepData) {
    const res = await api.put(`api/keeps/${keepId}`, keepData);
    return res.data;
  }
  async deleteKeep(keepId) {
    const res = await api.delete(`api/keeps/${keepId}`);
    logger.log("[KEEP DELETED ]", res.data);
    const keepToDelete = AppState.myKeeps.findIndex((v) => v.id == keepId);
    AppState.keeps.splice(keepToDelete, 1);
    AppState.myKeeps.splice(keepToDelete, 1);
    this.getKeeps();
    return res.data;
  }
}
export const keepsService = new KeepsService();
