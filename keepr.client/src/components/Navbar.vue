<template>
  <div class="flex justify-between items-center">
    <div id="home-create-buttons" class="p-0 m-0">
      <router-link class="p-0 m-0" :to="{ name: 'Home' }">
        <button class="btn btn-sm btn-outline elevation mx-3" title="Home Page">Home</button>
      </router-link>

      <!-- NOTE RE_USABLE DROPDOWN v -->
      <Dropdown v-if="user.isAuthenticated" align="left" width="10rem">
        <button class="btn btn-sm btn-ghost transition duration-200" title="Create menu">
          Create
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor"
            class="w-6 h-6">
            <path stroke-linecap="round" stroke-linejoin="round" d="M19.5 8.25l-7.5 7.5-7.5-7.5" />
          </svg>
        </button>
        <template #items>
          <DropdownitemGroup label="">
            <DropdownItem @click="toggleCreateKeep" as="button" description="">
              Create Keep
            </DropdownItem>

          </DropdownitemGroup>
          <DropdownitemGroup label="">
            <DropdownItem @click="toggleCreateVault" as="button" description="">
              Create Vault
            </DropdownItem>

          </DropdownitemGroup>
        </template>
      </Dropdown>
      <!-- NOTE RE_USABLE DROPDOWN ^ -->
    </div>
    <!-- KEEPR LOGO -->

    <div class="sm:inset-0 flex justify-center">
      <div class="h-12 rounded-sm my-1" :class="[user.isAuthenticated ? 'sm:mr-[9.5rem]' : '']">
        <img class="w-full h-full rounded elevation hover:animate-spin motion-reduce" src="../assets/img/KeeprLogo.png"
          alt="Logo" title="Keepr Co" />
      </div>
    </div>
    <Login class="" />
  </div>

  <NewKeepModal @create-keep="createNewKeep" @toggle-new-keep="toggleCreateKeep" v-model:isOpen="openNewKeep" />
  <NewVaultModal @toggle-new-vault="toggleCreateVault" v-model:isOpen="openNewVault" />
</template>

<script>
import Login from './Login.vue';
import { computed, ref } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import { ChevronDownIcon } from '@heroicons/vue/20/solid'
import Dropdown from "./Dropdown.vue"
import NewKeepModal from "./NewKeepModal.vue";
import NewVaultModal from "./NewVaultModal.vue";
import DropdownitemGroup from "./DropdownitemGroup.vue"
import { keepsService } from '../services/KeepsService.js'
import { vaultsService } from "../services/VaultsService.js";
export default {
  components: {
    NewKeepModal,
    NewVaultModal,

  },
  setup() {
    const openNewKeep = ref(false)
    const openNewVault = ref(false)
    return {
      openNewKeep,
      openNewVault,
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      toggleCreateKeep() {
        logger.log(`[TOGGLE NEW KEEP EVENT RECEIVED]`);

        openNewKeep.value = !openNewKeep.value

      },
      toggleCreateVault() {
        logger.log(`[TOGGLE NEW VAULT EVENT RECEIVED]`);

        openNewVault.value = !openNewVault.value
      },
      createNewKeep(keepData){
       const newKeep = keepsService.createKeep(keepData)
       logger.log("[KEEP CREATED] -", newKeep)
       openNewKeep.value = false
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
.logo-animate:hover {
  box-shadow: 5px 5px 6px rgba(0, 0, 0, .6);
}

.elevation {
  box-shadow: 5px 5px 6px rgba(0, 0, 0, .6);
}
</style>
