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
              class="bg-white overflow-hidden p-0 align-middle rounded-lg elevation transition-all max-w-[60rem] min-h-[20rem] max-h-full sm:max-h-[40rem]">



              <!-- NOTE MODAL FORM START-->
              <!-- SECTION MODAL FORM GUTS-->
              <section id="modal-guts">
                <div class="flex flex-col min-h-full p-3">
                  <p class="text-zinc-500 text-lg font-bold text-start mb-5">Add your keep</p>
                  <div class="flex flex-col space-y-5">

                    <input required v-model="vaultData.name" maxlength="20" id="Vault name" type="text"
                      placeholder="Vault name" class="min-w-[20rem] input input-bordered input-sm w-full max-w-xs"
                      title="vault name" />
                    <input required v-model="vaultData.img" type="text" placeholder="Vault image url" maxlength="400"
                      class="input input-bordered input-sm w-full max-w-xs" title="image url" />
                    <textarea required v-model="vaultData.description" maxlength="100" placeholder="Vault description"
                      class="min-h-[7rem] p-1 text-sm textarea textarea-bordered textarea-lg w-full max-w-xs"
                      title="description"></textarea>
                    <div class="form-control bg-slate-100">
                    <label class="label cursor-pointer">
                      <span class="label-text">is the Vault private?</span>
                      <input id="isPrivate" type="checkbox" checked="checked" class="checkbox border border-black indeterminate:bg-gray-300 valid:border-green-500" />
                    </label>
                  </div>
                  <!-- <input v-model="vaultData.isPrivate" type="checkbox" checked="false" class="checkbox" title="is Private?" /> -->
                  <button @click="createVault()" class="btn btn-neutral btn-sm">create</button>
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

const emit = defineEmits(['toggle-new-vault', 'create-vault']);
const props = defineProps({
  isOpen: Boolean,

})
const vaultData = ref({ name: "", img: "", description: ""})
const toggleModal = () => {
  console.log(`[event triggered]`)
  emit('toggle-new-vault');
}
function createVault() {

  logger.log(vaultData.name)
  if (vaultData.value.name == "" | vaultData.value.image == "" | vaultData.value.description == "") {
    Pop.error("All fields are required to create a vault, please fill out the form appropriately.")
    return
  }
  emit('create-vault', vaultData.value)
}
</script>


<style lang="scss" scoped></style>