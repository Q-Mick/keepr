<template>
  <TransitionRoot :show="isOpen" as="template" enter="duration-300 ease-out" enter-from="opacity-0" enter-to="opacity-100"
    leave="duration-200 ease-in" leave-from="opacity-100" leave-to="opacity-0">
    <Dialog as="div" @close="toggleModal" class="relative z-10">
      <TransitionChild as="template" enter="duration-300 ease-out" enter-from="opacity-0" enter-to="opacity-100"
        leave="duration-200 ease-in" leave-from="opacity-100" leave-to="opacity-0">
        <div class="fixed inset-0 bg-black bg-opacity-25" />
      </TransitionChild>

      <div class="fixed inset-0">
        <div class="flex min-h-full items-center justify-center p-4 text-center">
          <TransitionChild as="template" enter="duration-300 ease-out" enter-from="opacity-0 scale-95"
            enter-to="opacity-100 scale-100" leave="duration-200 ease-in" leave-from="opacity-100 scale-100"
            leave-to="opacity-0 scale-95">
            <DialogPanel as="div"
              class="bg-white overflow-auto sm:overflow-hidden p-0 align-middle rounded-lg elevation transition-all max-w-[60rem] min-h-[20rem] sm:max-h-[40rem] max-h-[95vh] ">


              <div @click="toggleModal" class="sm:hidden absolute transition-all pt-1 right-6 cursor-pointer">X</div>
              <!-- NOTE MODAL CONTAINER START -->
              <div class="flex flex-col sm:flex-row min-h-full">
                <!-- SECTION left Modal Pane -->
                <div id="keep-image" class="sm:w-1/2 relative">
                  <button v-if="user.id == keep.creatorId" @click="deleteKeep(keep.id)" title="Delete Keep"
                    class="absolute top-0 right-0 m-2 btn btn-error text-[10px] btn-xs focus:outline-none sm:btn-small">Delete
                    keep</button>
                  <div class="aspect-square w-full">
                    <img :src="keep.img" class="object-center w-full h-[100%] sm:h-full  sm:rounded-l-lg" alt="keep image"
                      @click="toggleModal">
                  </div>
                </div>
                <!-- right modal pane -->
                <div class="w-[100%] sm:w-1/2 p-3 sm:p-0">
                  <div class="flex flex-col justify-between items-center min-h-full">
                    <!-- TOP -->
                    <div id="top items" class="pt-0 sm:pt-5">

                      <div class="flex space-x-3 mt-1 mb-auto grow">
                        <i class="mdi mdi-eye" title="viewed">{{ keep.views }}</i>
                        <!-- NOTE LOGO NOT LEFT PICTURE -->
                        <div class="flex space-x-1" title="Kept count">

                          <img class="aspect-square w-5 h-5" src="../assets/img/K-Logo.png" alt="keepr logo">
                          <i class="" title="Kept">{{ keep.kept }}</i>
                        </div>
                      </div>

                    </div>
                    <!-- MID -->
                    <div id="middle-items" class="p-0 sm:p-10">

                      <p class="text-[35px] font-serif text-center font-bold">{{ keep.name }}</p>
                      <p class="text-left line-clamp-6">{{ keep.description }}</p>

                    </div>
                    <!-- BOTTOM -->
                    <div id="bottom-items"
                      class="flex flex-row-reverse self-center sm:flex-row w-full justify-between px-1 sm:px-8 pb-1 sm:pb-4'">


                      <div v-if="!vaultDisplay" class="flex space-x-2 text-center items-end sm:items-center">


                        <select v-model="selectedVault" :disabled="!loggedIn"
                          class="py-0 bg-slate-100 select select-xs w-full max-w-[6.5rem] focus:outline-none focus:border-none focus:ring-neutral">
                          <option disabled selected value="0" class="font-bold">Vaults</option>
                          <option v-for="v in vault" :key="v.id" :value="v.id">{{ v.name }}</option>
                        </select>

                        <button @click="addKeepToVault()" :disabled="!loggedIn"
                          class="btn btn-neutral btn-xs sm:btn-small">Save</button>
                      </div>

                      <div v-else-if="user.id == activeVault.creatorId" class="flex items-center">
                        <button @click="removeKeepFromVault(keep.vaultKeepId)" title="Remove keep from vault"
                          class="border-b border-zinc-700 m-0 p-0"><i class="mdi mdi-cancel">Remove</i></button>
                      </div>

                      <div
                        :class="[ 'flex flex-col-reverse sm:flex-row text-center items-start sm:items-center', user.id != keep.creatorId ? '' : '' ]">

                        <p class="pr-2 text-sm font-bold hidden sm:inline">{{ keep.creator.name }}</p>
                        <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                          <div class="avatar mt-2 sm:mt-0" :title="keep.creator.name" role="button">
                            <div class="w-12 border-black border rounded-lg shadow-md shadow-black elevation" role="img">
                              <img :src="keep.creator.picture" alt="creator picture" role="presentation" class="" />
                            </div>
                          </div>
                        </router-link>


                      </div>


                    </div>

                  </div>
                </div>
              </div>
              <!-- !SECTION end modal guts -->

            </DialogPanel>
          </TransitionChild>
        </div>
      </div>
    </Dialog>
  </TransitionRoot>
</template>


<script setup>
import { ref, watchEffect, computed } from 'vue'
import { Dialog, DialogPanel, DialogTitle, TransitionChild, TransitionRoot } from '@headlessui/vue'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { vaultsService } from "../services/VaultsService.js"
import { keepsService } from '../services/KeepsService.js'
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { PlusIcon } from '@heroicons/vue/20/solid'
import { Keep } from "../models/Keep.js";
const user = computed(() => AppState.account);
const loggedIn = computed(() => AppState.user.isAuthenticated)
const activeVault = computed(() => AppState.actVault)
const selectedVault = ref('0')
const vaults = ref(props.vault);
const vaultKeepData = { keepId: "", vaultId: "" }
const emit = defineEmits([ 'toggle-details', 'remove-keep' ]);
const props = defineProps({
  vaultDisplay: { type: Boolean },
  isOpen: Boolean,
  keep: {
    type: Object,
    required: true,
  },
  vault: {
    type: Array,
    required: true,
  }
})
async function addKeepToVault() {

  vaultKeepData.keepId = props.keep.id
  vaultKeepData.vaultId = selectedVault.value
  const newVaultKeep = await vaultKeepsService.createVaultKeep(vaultKeepData)
  // AppState.vaultKeeps.push(AppState.actKeep)
  return newVaultKeep
}

async function deleteKeep(keepId) {
  try {
    if (await Pop.confirm('Are you sure you want to delete the keep?')) {
      await keepsService.deleteKeep(keepId)
      emit('toggle-details');
    } else {

      return
    }
  } catch (error) {
    console.error(error);
  }
}
async function removeKeepFromVault(vaultKeepId) {
  try {
    if (await Pop.confirm('Are you sure you want to remove the keep from the vault?')) {
      await vaultKeepsService.deleteKeepFromVault(vaultKeepId)
      emit('toggle-details');
    } else {

      return
    }
  } catch (error) {
    console.error(error);
  }
}
const toggleModal = () => {
  console.log(`[event triggered]`)
  emit('toggle-details');
}

</script>


<style lang="scss" scoped>
.elevation {
  box-shadow: 4px 4px 6px rgba(0, 0, 0, .6);
}
</style>