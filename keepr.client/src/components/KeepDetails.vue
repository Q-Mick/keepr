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
                <div id="keep-image" class="sm:w-1/2">
                  <div class="aspect-square w-full">
                    <img :src="keep.img" class="object-center w-full h-[100%] sm:h-full  sm:rounded-l-lg"
                      alt="keep image">
                  </div>
                </div>
                <!-- right modal pane -->
                <div class="w-[100%] sm:w-1/2 p-3 sm:p-0">
                  <div class="flex flex-col justify-between items-center min-h-full">
                    <!-- TOP -->
                    <div id="top items" class="pt-0 sm:pt-5">

                      <div class="flex space-x-3 mt-1 mb-auto grow">
                        <i class="mdi mdi-eye">{{ keep.views }}</i>
                        <!-- NOTE LOGO NOT LEFT PICTURE -->
                        <div class="flex space-x-1">

                          <img class="aspect-square w-5 h-5" src="../assets/img/K-Logo.png" alt="keepr logo">
                          <i class=" ">{{ keep.kept }}</i>
                        </div>
                      </div>

                    </div>
                    <!-- MID -->
                    <div id="middle-items" class="p-0 sm:p-10">

                      <p class="text-[35px] font-serif text-center font-bold">{{ keep.name }}</p>
                      <p class="text-left">{{ keep.description }}</p>

                    </div>
                    <!-- BOTTOM -->
                    <div id="bottom-items"
                      class="flex flex-row-reverse sm:flex-row w-full justify-between px-1 sm:px-8 pb-1 sm:pb-4 ">

                      <div class="flex space-x-2 text-center items-end sm:items-center">
                        <!-- <button class="btn btn-xs sm:btn-small">Vaults</button> -->
                        <select class="select select-bordered select-xs w-full max-w-xs focus:outline-none">
                          <option disabled selected>Tiny</option>
                          <option>Tiny Apple</option>
                          <option>Tiny Orange</option>
                          <option>Tiny Tomato</option>
                        </select>
                        <button class="btn btn-neutral btn-xs sm:btn-small">Save</button>
                      </div>

                      <div class="flex flex-col-reverse sm:flex-row text-center items-start sm:items-center">
                        
                        <p class="pr-2 text-sm font-bold hidden sm:inline">{{ keep.creator.name }}</p>
                        <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId }}">
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
import { ref } from 'vue'
import { Dialog, DialogPanel, DialogTitle, TransitionChild, TransitionRoot } from '@headlessui/vue'
import { PlusIcon } from '@heroicons/vue/20/solid'
import { Keep } from "../models/Keep.js";
const open = ref(true)
const emit = defineEmits(['toggle-details']);
const props = defineProps({
  isOpen: Boolean,
  keep: {
    type: Object,
    required: true,
  }
})

const toggleModal = () => {
  console.log(`[event triggered]`)
  emit('toggle-details');
}
  // function setIsOpen(value) {
  //   isOpen.value = value
  // }
</script>


<style lang="scss" scoped>
.elevation {
  box-shadow: 4px 4px 6px rgba(0, 0, 0, .6);
}
</style>