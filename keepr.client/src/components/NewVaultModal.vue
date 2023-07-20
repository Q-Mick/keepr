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
              <section id="modal-guts" class="bg-zinc-100">
                <div class="flex flex-col min-h-full p-3">
                  <p class="text-zinc-500 text-lg font-bold text-start mb-5">Add your keep</p>
                  <div class="flex flex-col space-y-5">

                    <input required v-model="vaultData.name" maxlength="20" id="Vault name" type="text"
                      placeholder="Vault name" class="ring-[1px] ring-black min-w-[20rem] input input-bordered input-sm w-full max-w-xs"
                      title="vault name" />


                    <input required v-model="vaultData.img" type="url" placeholder="Vault image url" maxlength="400"
                      class="ring-[1px] ring-black input input-bordered input-sm w-full max-w-xs" title="image url" />


                    <textarea required v-model="vaultData.description" maxlength="100" placeholder="Vault description"
                      class="ring-[1px] ring-black min-h-[7rem] pl-3 py-1 text-sm textarea textarea-bordered textarea-lg w-full max-w-xs"
                      title="description"></textarea>


                    <div class="flex flex-col flex-grow-0">
                      <div class="flex flex-col">
                        <p class="text-[10px] p-0 my-0 py-1 mr-1 text-end">Private vaults can only be seen by you.</p>
                        <div class="inline-flex items-end justify-end">
                              <input v-model="vaultData.isPrivate" id="isPrivate" type="checkbox" title="private vault selector" class="h-5 w-5  border-black text-black  bg-slate-200 checkbox rounded-none default:ring-2 focus:ring-0" />
                    
                     
                          <span class="label-text ml-0.5">Make Vault Private?</span>
                          
                        </div>
                        <button @click="createVault()" class="btn btn-neutral btn-sm ml-auto w-[47%] py-1 my-1" title="Create Vault">Create Vault</button>
                      </div>

                    </div>
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
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";

const emit = defineEmits(['toggle-new-vault', 'create-vault']);
const props = defineProps({
  isOpen: Boolean,

})
const vaultData = ref({ name: "", img: "", description: "", isPrivate: false })
const toggleModal = () => {
  console.log(`[event triggered]`)
  emit('toggle-new-vault');
}
function createVault() {

  logger.log(vaultData.value)
  if (vaultData.value.name == "" | vaultData.value.img == "" | vaultData.value.description == "") {
    Pop.error("All fields are required to create a vault, please fill out the form appropriately.")
    return
  }
  logger.log("made it past pop")
  emit('create-vault', vaultData.value)
  emit('toggle-new-vault')
  vaultData.value = {name:"", img:"", description:"", isPrivate: false}
}
</script>


<style lang="scss" scoped>


</style>