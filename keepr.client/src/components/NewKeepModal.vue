<template>
 <TransitionRoot as="template" :show="isOpen">
      <Dialog  as="div" @close="toggleModal" class="relative z-10">
        <TransitionChild as="template" enter="ease-out duration-300" enter-from="opacity-0" enter-to="opacity-100"
          leave="ease-in duration-200" leave-from="opacity-100" leave-to="opacity-0">
          <div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" />
        </TransitionChild>
     

      <div class="fixed inset-0 z-10">
        <div class="flex min-h-full items-center justify-center p-4 text-center">
          <TransitionChild as="template" enter="ease-out duration-300"
            enter-from="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
            enter-to="opacity-100 translate-y-0 sm:scale-100" leave="ease-in duration-200"
            leave-from="opacity-100 translate-y-0 sm:scale-100"
            leave-to="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95">
            <DialogPanel
              class="relative bg-white overflow-hidden p-0 align-middle elevation transition-all max-w-[60rem] min-h-[20rem] max-h-full sm:max-h-[40rem]">



              <!-- NOTE MODAL FORM START-->
              <!-- SECTION MODAL FORM GUTS-->
              <section id="modal-guts">
                <div class="flex flex-col min-h-full p-3">
                  <p class="text-zinc-500 text-lg font-bold text-start mb-5">Add your keep</p>
                  <div class="flex flex-col space-y-5">

                    <input required v-model="keepData.name" maxlength="20" id="title" type="text" placeholder="keep title" class="min-w-[20rem] input input-bordered input-sm w-full max-w-xs" />
                    <input required v-model="keepData.img" type="text" placeholder="keep image url" maxlength="400" class="input input-bordered input-sm w-full max-w-xs" />
                    <textarea required v-model="keepData.description" placeholder="Keep description" class=" min-h-[7rem] p-1 text-sm textarea textarea-bordered textarea-lg w-full max-w-xs" ></textarea>
                    <button @click="createKeep()" class="btn btn-neutral btn-sm">create</button>
                  </div>
                </div>
                
              </section>
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
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";

const emit = defineEmits(['toggle-new-keep', 'create-keep']);
const props = defineProps({
  isOpen: Boolean,

})
const keepData = ref({name: "", img: "", description: "" })
function toggleModal(){
  console.log(`[event triggered]`)
  emit('toggle-new-keep')
}
function createKeep(){
  
  logger.log(keepData.name)
  if (keepData.value.name == "" | keepData.value.image == "" | keepData.value.description == "") {
    Pop.error("All fields are required to create a keep, please fill out the form appropriately.")
    return
  }
  emit('create-keep', keepData.value)
}
</script>


<style lang="scss" scoped></style>