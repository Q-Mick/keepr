<template>
  <div class="container mx-1 sm:mx-auto">

    <div class="w-fit mr-2 sm:mr-0">

      <div class="columns-2 sm:columns-4 gap-2 sm:gap-8 pt-5 mb-5">
        <Keep @click="setActiveKeep(keep.id)" v-for="keep in keeps" :key="keep.id" :keep="keep" class="my-5"
          :style="{ marginTop: keep.id === keeps[0].id ? '0' : 'auto' }" />
      </div>

    </div>


  </div>

<KeepDetails @toggle-details="closeModal" v-model:isOpen="openDetails" v-model:keep="actKeep" />
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
    let openDetails = ref(false)
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
      openDetailsModal: computed(() => openDetails.value),
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
