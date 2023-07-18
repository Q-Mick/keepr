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
      <div class="grid grid-cols-4">
        <p v-for="vault in vaults" :key="vault.id" class="my-5 border border-zinc-600">{{ vault.name }}</p>
      </div>
      <div class="">
          <p class="text-4xl sm:text-5xl  font-serif font-extrabold text-start mt-5">Keeps</p>
        </div>
      <div class="columns-2 sm:columns-4 gap-3 px-1 sm:pr-0 sm:gap-8 pt-5 mb-5">
        <Keep @click="setActiveKeep(keep.id)" v-for="keep in keeps" :key="keep.id" :keep="keep" class="my-5 border border-zinc-600"
          :style="{ marginTop: keep.id === keeps[0].id ? '0' : 'auto' }" />
      </div>
    </div>
      <div class="w-3 sm:w-[20%]"></div>
    </div>
</template>

<script>
import { onMounted, ref, watchEffect} from "vue";
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import KeepDetails from "../components/KeepDetails.vue";
export default {
  setup() {

    return {
      account:computed(() => AppState.account),
      keeps:computed(() => AppState.myKeeps),
      vaults:computed(() => AppState.myVaults),
    }
  }
}
</script>

<style scoped>
.elevation {
  box-shadow: 2px 5px 4px rgba(0, 0, 0, .6);
}
</style>
