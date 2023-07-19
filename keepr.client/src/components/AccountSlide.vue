<template>
  <TransitionRoot as="template" :show="open">
    <Dialog as="div" class="relative z-10" @close="closeSlide">
      <div class="fixed inset-0" />

      <div class="fixed inset-0 overflow-hidden">
        <div class="absolute inset-0 overflow-hidden">
          <div class="pointer-events-none fixed inset-y-0 right-0 flex max-w-full pl-10 sm:pl-16">
            <TransitionChild as="template" enter="transform transition ease-in-out duration-500 sm:duration-700"
              enter-from="translate-x-full" enter-to="translate-x-0"
              leave="transform transition ease-in-out duration-500 sm:duration-700" leave-from="translate-x-0"
              leave-to="translate-x-full">
              <DialogPanel class="pointer-events-auto w-screen max-w-md">
                <div class="flex h-full flex-col overflow-y-scroll bg-white shadow-xl">
                  <div class="px-4 py-6 sm:px-6">
                    <div class="flex items-start justify-between">
                      <h2 id="slide-over-heading" class="text-base font-semibold leading-6 text-gray-900">Account</h2>
                      <div class="ml-3 flex h-7 items-center">
                        <button type="button"
                          class="rounded-md bg-white text-gray-400 hover:text-gray-500 focus:ring-2 focus:ring-indigo-500"
                          @click="closeSlide">
                          <span class="sr-only">Close panel</span>
                          <XMarkIcon class="h-6 w-6" aria-hidden="true" />
                        </button>
                      </div>
                    </div>
                  </div>
                  <!-- Main -->
                  <div>
                    <div class="pb-1 sm:pb-6">
                      <div>
                        <div class="relative h-40 sm:h-56">
                          <img class="absolute h-full w-full object-cover" :src="account.picture" alt="" />
                        </div>
                        <div class="mt-6 px-4 sm:mt-8 sm:flex sm:items-end sm:px-6">
                          <div class="sm:flex-1">
                            <div>
                              <div class="flex items-center">
                                <h3 class="text-xl font-bold text-gray-900 sm:text-2xl">{{ account.name }}</h3>
                                <span class="ml-2.5 inline-block h-2 w-2 flex-shrink-0 rounded-full bg-green-400">
                                  <span class="sr-only">Online</span>
                                </span>
                              </div>
                              <p class="text-sm text-gray-500">{{ account.email }}</p>
                            </div>

                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="px-4 pb-5 pt-5 sm:px-0 sm:pt-0">
                      <dl class="space-y-8 px-4 sm:space-y-6 sm:px-6">
                        <div>
                          <dt class="text-sm font-medium text-gray-500 sm:w-40 sm:flex-shrink-0">Bio</dt>
                          <dd class="mt-1 text-sm text-gray-900 sm:col-span-2">
                            <p>{{ account.bio }}</p>
                          </dd>
                        </div>
                        <div>
                          <!-- NOTE edit account section -->
                          <form class="" @submit.prevent="handleSubmit">

                            <div class="relative mb-3">
                              <label for="name"
                                class="absolute -top-2 left-2 inline-block bg-white px-1 text-xs font-medium text-gray-900">Name</label>
                              <input v-model="editable.name" type="text" name="name" id="name" maxlength="50"
                                class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                placeholder="Jane Smith" />
                            </div>
                            <!-- <div class="input-group mb-3">
              <span class="input-group-text" id="basic-addon1">Name:</span>
              <input name="name" class="input-text form-control" placeholder="Name" type="text" required
                v-model="editable.name">
            </div> -->

                            <div class="relative">
                              <label for="picture"
                                class="absolute -top-2 left-2 inline-block bg-white px-1 text-xs font-medium text-gray-900">Picture</label>
                              <input v-model="editable.picture" type="text" maxlength="125" name="name" id="name"
                                class="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                                placeholder="Jane Smith" />
                            </div>

                            <!-- <div class="input-group mb-3">
              <span class="input-group-text" id="basic-addon1">Profile Banner:</span>
              <input name="cover-img" class="input-text form-control" placeholder="Cover image" type="url" required
                v-model="editable.coverImg">
            </div> -->



                            <div class="text-end">
                              <button type="submit"
                                class="mt-3 inline-flex items-center gap-x-1.5 rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">
                                Save Account
                                <CheckCircleIcon class="-mr-0.5 h-5 w-5" aria-hidden="true" />
                              </button>


                            </div>

                          </form>

                        </div>
                      </dl>
                    </div>
                  </div>
                </div>
              </DialogPanel>
            </TransitionChild>
          </div>
        </div>
      </div>
    </Dialog>
  </TransitionRoot>
</template>


<script setup>
// NOTE these are required to use tailwindui components
import { CheckCircleIcon } from '@heroicons/vue/20/solid'
import { AppState } from "../AppState.js";
import { ref, watchEffect } from "vue";
import { Account } from "../models/Account.js";
import Pop from "../utils/Pop.js";
import {
  Dialog,
  DialogPanel,
  Menu,
  MenuButton,
  MenuItem,
  MenuItems,
  TransitionChild,
  TransitionRoot,
} from '@headlessui/vue'
import { XMarkIcon } from '@heroicons/vue/24/outline'
import { EllipsisVerticalIcon } from '@heroicons/vue/20/solid'

const props = defineProps({
  open: Boolean,
  account: Object,
})
const emit = defineEmits(['close-slide', 'submit-account']);
const closeSlide = () => {
  emit('close-slide');
}
const editable = ref({ ...AppState.account });

const handleSubmit = () => {
  Pop.toast('updating account');
  emit('submit-account', editable.value);
};

watchEffect(() => {
  editable.value = { ...AppState.account };
});

</script>


<style lang="scss" scoped></style>