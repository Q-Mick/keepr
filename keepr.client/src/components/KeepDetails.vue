<template>
  <!-- Wrap your dialog in a `TransitionRoot` to add transitions. -->
  <TransitionRoot :show="isOpen" as="template" enter="duration-300 ease-out" enter-from="opacity-0" enter-to="opacity-100"
    leave="duration-200 ease-in" leave-from="opacity-100" leave-to="opacity-0">
    <Dialog as="div" @close="toggleModal" class="relative z-10">
      <TransitionChild as="template" enter="duration-300 ease-out" enter-from="opacity-0" enter-to="opacity-100"
        leave="duration-200 ease-in" leave-from="opacity-100" leave-to="opacity-0">
        <div class="fixed inset-0 bg-black bg-opacity-25" />
      </TransitionChild>

      <div class="fixed inset-0 overflow-y-auto">
        <div class="flex min-h-full items-center justify-center p-4 text-center">
          <TransitionChild as="template" enter="duration-300 ease-out" enter-from="opacity-0 scale-95"
            enter-to="opacity-100 scale-100" leave="duration-200 ease-in" leave-from="opacity-100 scale-100"
            leave-to="opacity-0 scale-95">
            <DialogPanel
              class="w-full max-w-[50rem] h-[30rem] transform overflow-hidden rounded-2xl bg-white p-0 text-left align-middle shadow-xl transition-all">
              
                <img class="h-full aspect-square w-[50%]" :src="keep.img" alt="Movie" />
                <div class="card-body">
                  <DialogTitle as="h3" class="text-lg font-medium leading-6 text-gray-900">
                    {{ keep.name }}
                  </DialogTitle>
                  <p class="">{{ keep.description }}</p>
                  <p>Test</p>
                  <div class="card-actions justify-end">
                    <button class="btn btn-primary">Save</button>
                  </div>
                </div>
              
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
    type: Keep,
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


<style lang="scss" scoped></style>