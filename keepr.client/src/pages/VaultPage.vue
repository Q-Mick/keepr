<template>
  <section id="vaultImageSection">

    <div class="relative mx-auto max-w-7xl text-center px-10 mt-5 h-[15rem] overflow-hidden">
      <img :src="vault?.img" alt="Vault image" :title="vault?.name" class="aspect-video h-full mx-auto rounded-lg">
      <p class="absolute inset-36 text-white font-serif font-boldest text-4xl custom-text-shadow">{{ vault?.name }}</p>
      <p class="absolute inset-44 text-white font-serif">By {{ vault?.creator?.name }}</p>
    </div>
  </section>
</template>


<script>
import { onMounted, popScopeId, ref, watchEffect} from "vue";
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { keepsService } from '../services/KeepsService.js'
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import { useRoute } from 'vue-router';
import Keep from "../components/Keep.vue";
import Vault from "../components/Vault.vue";
import Pop from "../utils/Pop.js";
import { router } from "../router.js";

export default {
  setup(){
    const route = useRoute()
    
    onMounted(() => setActiveVault())
    async function setActiveVault() {
      try {
        const vaultId = route.params.vaultId
        await vaultsService.setActiveVault(vaultId);
        if (AppState.actVault.isPrivate == true) {
          if (AppState.user.id == AppState.actVault.creatorId) {
            return
          } else {
            Pop.error("This vault is private and not yours. Stop being nosy.")
            router.push('Home')
          }
        }
      }
      catch (error) {
        logger.log(error.message);
      }
    }
    async function deleteVault(vaultId) {
    try {
      if (await Pop.confirm('Are you sure you want to delete the vault?')){
        await vaultsService.deleteKeep(vaultId)
        emit('toggle-details');
      } else {
        
        return
      }
    } catch (error) {
      console.error(error);
    }
  }
    return {
      vault: computed(() => AppState.actVault),
    }
  }

}
</script>


<style lang="scss" scoped>
.custom-text-shadow {
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 1.0);
}
</style>