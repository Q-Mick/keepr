<template>
  <div class="container mx-1 sm:mx-auto">

    <div class="w-fit mr-2 sm:mr-0">

      <div class="columns-2 sm:columns-4 gap-3 px-1 sm:pr-0 sm:gap-8 pt-5 mb-5">
        <Keep @click="setActiveKeep(keep.id)" v-for="keep in keeps" :key="keep.id" :keep="keep"  class="min-h-[6rem] my-5 border border-zinc-600"
          :style="{ marginTop: keep.id === keeps[0].id ? '0' : 'auto' }" />
      </div>

    </div>


  </div>

  <KeepDetails @toggle-details="closeModal" v-model:isOpen="openDetails" v-model:keep="actKeep" :vault="myVaults" :vaultDisplay="false" />
</template>

<script>
import { computed, onMounted, watchEffect, ref } from "vue";
import { AppState } from "../AppState.js";
import { AuthService } from '../services/AuthService'
import { logger } from "../utils/Logger.js";
import { keepsService } from '../services/KeepsService.js'
import { accountService } from "../services/AccountService.js";
import Keep from '../components/Keep.vue';
import KeepDetails from "../components/KeepDetails.vue";
export default {
  components: {
    Keep,
    KeepDetails,
  },
  setup() {
    onMounted(() => getKeeps())
    const openDetails = ref(false)
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        logger.log(error.message);
      }
    }
 
    return {
      actKeep: computed(() => AppState.actKeep),
      keeps: computed(() => AppState.keeps),
      myVaults: computed(() => AppState.myVaults),
      openDetailsModal: computed(() => openDetail.value),
      openDetails,
      getKeeps,
      async setActiveKeep(keepId) {
        logger.log(`[FUNCTION - OPEN DETAILS MODAL] Payload --->`, keepId);
        await keepsService.setActiveKeep(keepId);
        openDetails.value = true
},
        closeModal() {
        logger.log(`[CLOSE MODAL EVENT RECEIVED]`);

        openDetails.value = false
  
}

    };
  },
}
</script>

<style scoped lang="scss"></style>
