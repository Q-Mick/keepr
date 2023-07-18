<template>
  <div class="container min-w-full">
    <div class="flex flex-col justify-center items-center align-middle mt-7">
<div class="relative w-full px-5 sm:px-0 sm:w-2/5 h-96">
  <img class="h-full w-full aspect-video" :src="account.coverImg" alt="">
  <div class="avatar absolute bottom-[-2rem] left-[45%] rounded-full border border-black elevation">
    <div class="w-[5rem] rounded-full">
    <img :src="account.picture" alt="" title="Account Picture">
  </div>
</div>
</div>
<div class="">...</div>
<div class="">Column 3</div>


    </div>
  </div>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState';
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
.elevation {
  box-shadow: 2px 3px 4px rgba(0, 0, 0, .6);
}</style>
