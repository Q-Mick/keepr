<template>
  <div class="container">

    <div class="content-center text-center">
      <h1>Welcome {{ account.name }}</h1>
      <div class="avatar">
  <div class="w-24 rounded-full">
    <img :src="account.picture" />
  </div>
</div>
      <p>{{ account.email }}</p>
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

</style>
