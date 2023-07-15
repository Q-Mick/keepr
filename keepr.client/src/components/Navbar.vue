<template>
  <div class="flex justify-between">
    <div id="home-create-buttons">
      <router-link class="" :to="{ name: 'Home' }">
        <button class="btn btn-sm btn-neutral mt-3 mx-3" title="Home Page">Home</button>
      </router-link>

      <!-- NOTE RE_USABLE DROPDOWN -->
      <Dropdown align="left" width="10rem">
      <button class="btn btn-sm btn-ghost" title="Create menu">
        Create
        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor"
          class="w-6 h-6">
          <path stroke-linecap="round" stroke-linejoin="round" d="M19.5 8.25l-7.5 7.5-7.5-7.5" />
        </svg>
      </button>
      <template #items>
        <DropdownitemGroup label="">
            <DropdownItem as="button" description="">
              Create Keep
            </DropdownItem>

          </DropdownitemGroup>
          <DropdownitemGroup label="">
            <DropdownItem as="button" description="">
              Create Vault
            </DropdownItem>

          </DropdownitemGroup>
      </template>
    </Dropdown>
    </div>
    <!-- KEEPR LOGO -->
 
<div class="h-12 mt-[5px] m-0 sm:mr-[7rem] overflow-hidden">
  <img class="w-full h-full object-cover" src="../assets/img/KeeprLogo.png" />
</div>


 
    <Login />
  </div>
</template>

<script>
import Login from './Login.vue';
import { ChevronDownIcon } from '@heroicons/vue/20/solid'
import Dropdown from "./Dropdown.vue"

import DropdownitemGroup from "./DropdownitemGroup.vue"
export default {
  setup() {
    return {}
  },
  components: { Login }
}
</script>

<style scoped></style>
