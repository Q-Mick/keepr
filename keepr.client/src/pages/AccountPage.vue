<template>
  <div class="flex">
  <div class="w-3 sm:w-[20%]"></div>
  <div class="w-full px-0 sm:px10 sm:w-[80%]">
    <div class="flex flex-col column-3 justify-center items-center align-middle mt-7">
   

        <!-- banner and pfp and edit acct btn -->
        <div class="w-full px-5 sm:px-0 sm:max-w-[50rem] h-96">
          <div class="relative h-full w-full">
            <img class="h-full w-full object-fit aspect-video sm:object-cover" :src="account.coverImg" alt="Banner" title="Account cover image">
            <div class="avatar absolute bottom-[-2rem] left-[39%] sm:left-[45%] rounded-full border border-white elevation">
              <div class="w-[5rem] rounded-full">
                <img :src="account.picture" alt="" title="Account Picture">
              </div>
            </div>
          </div>
          <!-- name and vault / keep count v -->
          <div class="flex justify-end">
            <button @click="openSlide" class="btn btn-xs btn-neutral mt-1">Edit</button>
          </div>
        </div>
          <p class="text-5xl font-serif font-extrabold text-center mt-9">{{ account?.name }}</p>
          <p></p>
        <div id="VKCount">
          <p class="text-xl font-serif font-thin text-center">{{ keeps.length }} keeps | {{ vaults.length }} Vaults</p>
        </div>
   
      </div>
      <div id="VaultsSection" class="flex">
        
        <div class="">
          <p class="text-4xl sm:text-5xl  font-serif font-extrabold text-start mt-5">Vaults</p>
        </div>
      </div>
      <div v-if="vaults.length != 0">
        <div class="grid grid-cols-2 sm:grid-cols-4 gap-x-5 gap-y-1">
          <Vault v-for="vault in vaults" :key="vault.id" :vault="vault" class="my-2 border min-h-full border-zinc-600">{{ vault.name }}</Vault>
        </div>
      </div>
      <div v-else>
        <p class="pl-3 pt-5">You have no vaults yet.</p>
      </div>
        <div class="">
          <p class="text-4xl sm:text-5xl  font-serif font-extrabold text-start mt-5">Keeps</p>
        </div>
      <div class="relative columns-2 sm:columns-4 gap-3 px-1 sm:pr-0 sm:gap-8 pt-5 mb-5">
        <Keep @click="setActiveKeep(keep.id)" v-for="keep in keeps" :key="keep.id" :keep="keep" :show-creator-pic="false" class="my-5 border border-zinc-600"
          :style="{ marginTop: keep.id === keeps[0].id ? '0' : 'auto' }"></Keep>
      </div>
    </div>
      <div class="w-3 sm:w-[20%]"></div>
    </div>
    <KeepDetails @toggle-details="closeModal" v-model:vault="myVaults" v-model:isOpen="openDetails" v-model:keep="actKeep" />
    <AccountSlide v-model:open="open" v-bind:account="account" @close-slide="closeSlide" @submit-account="handleSubmit" />
</template>

<script>
import { onMounted, ref, watchEffect} from "vue";
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import KeepDetails from "../components/KeepDetails.vue";
import AccountSlide from "../components/AccountSlide.vue";
import { accountService } from "../services/AccountService.js";
import { keepsService } from '../services/KeepsService.js'
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Keep from "../components/Keep.vue";
import Vault from "../components/Vault.vue";
import Pop from "../utils/Pop.js";
export default {
  components: {
    AccountSlide,
    Keep,
    Vault,
    KeepDetails,
  },
  setup() {
    onMounted(() => getKeeps())
    const openAcct = ref(false)
    const openDetails = ref(false)
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        logger.log(error.message);
      }
    }
    async function handleSubmit(data) {
      // Handle the submitted account data from the child component
      logger.log('Submitted account:', data);
      try {
        Pop.toast('updating account')
        await accountService.editAccount(data)
      } catch (error) {
        Pop.error(error, '[Editing Account]')
      }
    }
    function openSlide(){
      openAcct.value = true; // Open the slide
    };
    function closeSlide(){
      openAcct.value = false; // Close the slide
    };
    return {
      handleSubmit,
      closeSlide,
      openSlide,
      openAcct,
      myVaults: computed(() => AppState.myVaults),
      open: computed(() => openAcct.value),
      user: computed(() => AppState.user),
      openDetails: computed(() => openDetails.value),
      account:computed(() => AppState.account),
      keeps:computed(() => AppState.myKeeps),
      actKeep: computed(() => AppState.actKeep),
      vaults:computed(() => AppState.myVaults),
            async setActiveKeep(keepId) {
        logger.log(`[FUNCTION - OPEN DETAILS MODAL] Payload --->`, keepId);
        await keepsService.setActiveKeep(keepId);
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

<style scoped>
.elevation {
  box-shadow: 2px 5px 4px rgba(0, 0, 0, .6);
}
</style>
