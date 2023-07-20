<template>
  <section v-if="account" id="vaultImageSection">
    <div class="relative mx-auto max-w-7xl text-center px-10 mt-5 h-[15rem] overflow-hidden">
      <img :src="vault?.img" alt="Vault image" :title="vault?.name" class="aspect-video h-full mx-auto rounded-lg">
      <p class="absolute inset-36 text-white font-serif font-boldest text-4xl custom-text-shadow">{{ vault?.name }}</p>
      <p class="absolute inset-44 text-white font-serif custom-text-shadow">By {{ vault?.creator?.name }}</p>
      
    </div>
    <div class="relative mx-auto text-center w-[50%]">
      <!-- keep count btn -->
      <button class="badge badge-ghost h-6 font-bold mx-auto my-1 cursor-default bg-zinc-300 ">{{ keeps.length }} Keeps in vault</button>
      <button @click="deleteVault(vault?.id)" class="btn btn-error btn-xs rounded-full h-6 font-bold mx-auto my-1 cursor-pointer " title="Delete vaults">Delete Vault</button>
    </div>
    <p v-if="keeps.length == 0" class="mx-auto text-center mt-5 text-xl font-serif">This vault is empty. .</p>
    <div class="columns-2 sm:columns-4 gap-3 px-1 sm:pr-0 sm:gap-8 pt-5 mb-5 max-w-[75%] mx-auto">
        <Keep @click="setActiveVaultKeep(keep)" v-for="keep in keeps" :key="keep.id" :keep="keep" class="min-h-[5rem] my-5 border border-zinc-600"
          :style="{ marginTop: keep.id === keeps[0].id ? '0' : 'auto' }" :showCreatorPic="false" />
      </div>
  </section>
  <div v-else>Loading...</div>
  <KeepDetails @toggle-details="closeModal" v-model:isOpen="openDetails" v-model:keep="actKeep" :vault="myVaults" :vaultDisplay="true" />
</template>


<script>
import { onMounted, popScopeId, ref, watchEffect} from "vue";
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js'
import { vaultsService } from "../services/VaultsService.js";
import KeepDetails from "../components/KeepDetails.vue";
import { logger } from "../utils/Logger.js";
import { useRoute, useRouter } from 'vue-router';
import Keep from "../components/Keep.vue";
import Vault from "../components/Vault.vue";
import Pop from "../utils/Pop.js";
import { router } from "../router.js";

export default {
  setup(){
    const router = useRouter()
    const route = useRoute()
    const openDetails = ref(false)
    onMounted(() => setActiveVault())
    async function setActiveVault() {
      try {
        debugger
        const vaultId = route.params.vaultId
        await vaultsService.setActiveVault(vaultId);
        if (AppState.actVault.isPrivate == true) {
          if (AppState.user.id == AppState.actVault.creatorId) {
            return
          } else {
            Pop.error("This vault is private and not yours. Stop being nosy.")
            
          }
        }
      }
      catch (error) {
        logger.log(error.message);
        router.push({name: 'Home'})
      }
    }
    async function deleteVault(vaultId) {
    try {
      if (await Pop.confirm('Are you sure you want to delete the vault?')){
        logger.log("pushing route")
        router.push({path: '/Account'})
        await vaultsService.deleteVault(vaultId)
      
      } else {
        
        return
      }
    } catch (error) {
      console.error(error);
    }
  }
    return {
      deleteVault,
      openDetails,
      vault: computed(() => AppState.actVault),
      keeps: computed(() => AppState.vaultKeeps),
      actKeep: computed(() => AppState.activeVaultKeep),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      async setActiveVaultKeep(keep) {
        logger.log(`[FUNCTION - OPEN DETAILS MODAL] Payload --->`, keep.vaultKeepId);
        keepsService.setActiveVaultKeep(keep);
        openDetails.value = true
},
        closeModal() {
        logger.log(`[CLOSE MODAL EVENT RECEIVED]`);

        openDetails.value = false
  
}
    }
  }

}
</script>


<style lang="scss" scoped>
.custom-text-shadow {
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 1.0);
}
</style>