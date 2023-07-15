<template>
  <div class="">
    <button class="btn btn-primary btn-sm my-2 mx-2" @click="login" v-if="!user.isAuthenticated">
      Login
    </button>
    <div class="" v-else>
      <Dropdown align="right" width="10rem" class="mr-2 mt-[5px]">
        <div class="avatar" title="Account Options">
          <div class="w-12 rounded-full">
            <img :src="account.picture" />
          </div>
        </div>
        <!-- NOTE MENU ITEMS -->
        <template #items>
          <DropdownitemGroup label="Account options">
            <DropdownItem as="button" description="">
              My Profile
            </DropdownItem>
            <DropdownItem @click="logout" as="button" description="">
              Sign out
            </DropdownItem>
        </DropdownitemGroup>
        </template>
      </Dropdown>
      
    </div>
  </div>

</template>

<script setup>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
import Dropdown from "./Dropdown.vue"
import DropdownitemGroup from "./DropdownitemGroup.vue"
import DropdownItem from "./DropdownItem.vue"
'@headlessui/vue'


const user = computed(() => AppState.user)
const account = computed(() => AppState.account)

async function login() {
  AuthService.loginWithPopup()
}

async function logout() {
  AuthService.logout({ returnTo: window.location.origin })
}
</script>


<style lang="scss" scoped></style>
