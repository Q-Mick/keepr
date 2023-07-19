<template>
    <router-link :to="{ name: 'Vault', params: { vaultId: vault?.id }}" class="border-none">
    <div class="relative my-2 shadow-xl shadow-black elevation rounded-lg overflow-hidden cursor-pointer">
        <img class="rounded-lg w-full hover:scale-105 duration-150" :src="vault?.img" alt="vault image"
            :title="vault?.name">

        <h2 class="pl-2 pb-1 m-0 absolute left-0 bottom-0 text-white text-lg custom-text-shadow font-semibold font-serif">{{
            vault?.name }}</h2>
<div v-if="vault?.isPrivate" class="absolute right-0 bottom-0 pr-2 pb-[3px]">
  <div class="w-6 rounded-full border border-black avatar-elevation flex justify-center items-center">
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="currentColor" title="Vault is private" class="rounded-full bg-white w-6 h-6">
      <path fill-rule="evenodd" d="M12 1.5a5.25 5.25 0 00-5.25 5.25v3a3 3 0 00-3 3v6.75a3 3 0 003 3h10.5a3 3 0 003-3v-6.75a3 3 0 00-3-3v-3c0-2.9-2.35-5.25-5.25-5.25zm3.75 8.25v-3a3.75 3.75 0 10-7.5 0v3h7.5z" clip-rule="evenodd" />
    </svg>
  </div>
</div>


    </div>
</router-link>
</template>
<script setup>
import { Vault } from "../models/Vault.js";
import { vaultsService } from "../services/VaultsService.js";
import { AppState } from "../AppState.js";
const emit = defineEmits(['setActiveVault']);
const props = defineProps({
    vault: {
        type: Vault,
        required: true,
    },
});
async function setActiveVault(vaultId) {
    // console.log(`[FROM KEEP COMPONENT] payload = ${keepId}`)
    AppState.actVault = vaultsService.setActiveVault(vaultId)
}

</script>


<style lang="scss" scoped>
.custom-text-shadow {
    text-shadow: 2px 2px 4px rgba(0, 0, 0, 1.0);
}

.elevation {
    box-shadow: 10px 15px 6px rgba(0, 0, 0, .6);
}

.avatar-elevation {
    box-shadow: 5px 3px 6px rgba(0, 0, 0, .6);
}
</style>