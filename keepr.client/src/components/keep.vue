<template>
<div class="my-2 shadow-2xl">
  <img 
  class="rounded-lg w-full inset-0"
  :src="keep.img" alt="keep image" :title="keep.name">
</div>
</template>


<script setup>
import { Keep } from "../models/Keep.js";
const props = defineProps({
  keep: {
    type: Keep, 
    required: true,
  },
});
</script>


<style lang="scss" scoped>

</style>