<template>
  <div class="flex">
  <div class="w-3 sm:w-[20%]"></div>
  <div class="w-full px-0 sm:px10 sm:w-[80%]">
    <div class="flex flex-col column-3 justify-center items-center align-middle mt-7">
   

        <!-- banner and pfp and edit acct btn -->
        <div class="w-full px-5 sm:px-0 sm:max-w-[50rem] h-96">
          <div class="relative h-full w-full">
            <img class="h-full w-full object-fit aspect-video sm:object-cover" :src="profile.coverImg" alt="Banner" title="profile cover image">
            <div class="avatar absolute bottom-[-2rem] left-[39%] sm:left-[45%] rounded-full border border-white elevation">
              <div class="w-[5rem] rounded-full">
                <img :src="profile.picture" alt="" title="profile Picture">
              </div>
            </div>
          </div>
          <!-- name and vault / keep count v -->
        </div>
          <p class="text-5xl font-serif font-extrabold text-center mt-9">{{ profile?.name }}</p>
          <p></p>
        <div id="VKCount">
          <p class="text-xl font-serif font-thin text-center">{{ keeps.length }} keeps | {{ vaults.length }} Vaults</p>
        </div>
   
      </div>
      <div id="VaultsSection" class="flex">
        
        <div class="">
          <p class="text-4xl sm:text-5xl font-serif font-extrabold text-start mt-5">Vaults</p>
        </div>
      </div>
      <div v-if="vaults.length != 0">
        <div class="grid grid-cols-2 sm:grid-cols-4 gap-3">
          <Vault v-for="vault in vaults" :key="vault.id" :vault="vault" class="my-5 border border-zinc-600">{{ vault.name }}</Vault>
        </div>
      </div>
      <div v-else>
        <p class="pl-3 pt-5">This profile has no vaults</p>
      </div>
      <div class="">
          <p class="text-4xl sm:text-5xl  font-serif font-extrabold text-start mt-5">Keeps</p>
        </div>
      <div class="columns-2 sm:columns-4 gap-3 px-1 sm:pr-0 sm:gap-8 pt-5 mb-5">
        <Keep @click="setActiveKeep(keep.id)" v-for="keep in keeps" :key="keep.id" :keep="keep" :showCreatorPic="false" class="my-5 border border-zinc-600"
          :style="{ marginTop: keep.id === keeps[0].id ? '0' : 'auto' }" />
      </div>
    </div>
      <div class="w-3 sm:w-[20%]"></div>
    </div>
    <KeepDetails @toggle-details="closeModal" v-model:isOpen="openDetails" v-model:keep="actKeep" />
</template>

<script>
import { onMounted, ref, watchEffect} from "vue";
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import KeepDetails from "../components/KeepDetails.vue";
import { keepsService } from '../services/KeepsService.js'
import { vaultsService } from "../services/VaultsService.js";
import { useRoute } from 'vue-router';
import { logger } from "../utils/Logger.js";
import Keep from "../components/Keep.vue";
import Vault from "../components/Vault.vue";
import { accountService } from "../services/AccountService.js";
export default {
  components: {
    Keep,
    Vault,
    KeepDetails,
  },
  setup() {
    const route = useRoute()
    onMounted(() => getKeeps())
    onMounted(() => getProfile())
    onMounted(() => getProfileKeeps())
    onMounted(() => getProfileVaults())
    const openDetails = ref(false)
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        logger.log(error.message);
      }
    }
    async function getProfileKeeps() {
      try {
        const profileId = route.params.profileId
        await accountService.getProfileKeeps(profileId);
      }
      catch (error) {
        logger.log(error.message);
      }
    }
    async function getProfileVaults() {
      try {
        const profileId = route.params.profileId
        await accountService.getProfileVaults(profileId);
      }
      catch (error) {
        logger.log(error.message);
      }
    }
    async function getProfile() {
      try {
        const profileId = route.params.profileId
        await accountService.getProfile(profileId);
      }
      catch (error) {
        logger.log(error.message);
      }
    }
    return {
      openDetails: computed(() => openDetails.value),
      account:computed(() => AppState.account),
      keeps:computed(() => AppState.profileKeeps),
      actKeep: computed(() => AppState.actKeep),
      actVault: computed(() => AppState.actVault),
      profile: computed(() => AppState.activeProfile),
      vaults:computed(() => AppState.profileVaults),
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
