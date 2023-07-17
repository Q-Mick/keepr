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
              class="bg-white overflow-hidden p-0 align-middle elevation transition-all max-w-[60rem] min-h-[20rem] max-h-full sm:max-h-[40rem]">



              <!-- NOTE MODAL FORM START-->
              <!-- SECTION MODAL FORM GUTS-->
              <div class="flex flex-col min-h-full p-4">
        <p class="text-zinc-500 text-lg font-bold">Add your keep</p>
        <button @click="toggleModal" class="btn btn-neutral btn-sm">create</button>
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

const emit = defineEmits(['toggle-new-keep']);
const props = defineProps({
  isOpen: Boolean,

})

const toggleModal = () => {
  console.log(`[event triggered]`)
  emit('toggle-new-keep');
}
</script>


<style lang="scss" scoped>

</style>