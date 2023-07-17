<template>
  <div class="">
    <button class="btn btn-sm btn-outline elevation mt-3 mx-3" @click="login" v-if="!user.isAuthenticated" title="Login">
      Login
    </button>
    <div class="" v-else>
      <Dropdown align="right" width="10rem" class="mr-2 mt-[5px]">
        <div class="avatar" title="Account Options" role="button">
          <div class="w-12 border-black border rounded-full shadow-md shadow-black elevation" role="img">
            <img :src="account.picture" alt="profile picture" role="presentation" class="" />
          </div>
        </div>
        <!-- NOTE MENU ITEMS -->
        <template #items>
          <DropdownitemGroup label="Account options">
            <router-link :to="{ name: 'Account'}">
              <DropdownItem as="button" description="">
                My Profile
              </DropdownItem>
            </router-link>
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


<style lang="scss" scoped>
.elevation{
  box-shadow: 4px 4px 6px rgba(0, 0, 0, .6);
}
</style>
