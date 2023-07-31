<template>
  <div class="relative my-2 shadow-xl shadow-black elevation rounded-lg overflow-hidden cursor-pointer">
    <img class="rounded-lg w-full hover:scale-105 duration-150" :src="keep.img" alt="keep image" :title="keep.name">

    <h2 class="pl-2 pb-1 m-0 absolute left-0 bottom-0 text-white text-lg custom-text-shadow font-semibold font-serif">{{
      keep.name }}</h2>
    <router-link v-if="showCreatorPic" :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
      <div class="absolute avatar right-0 bottom-0 pr-2 pb-[3px] ">
        <div v-if="showCreatorPic" class="w-10 rounded-full border border-black avatar-elevation">
          <img :src="keep.creator.picture" alt="user image" :title="keep.creator.name" />
        </div>
      </div>
    </router-link>
  </div>
</template>


<script setup>
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { AppState } from "../AppState.js";
const emit = defineEmits(['toggle-details']);
const props = defineProps({
  keep: {
    type: Object,
    required: true,
  },
  showCreatorPic: {
    type: Boolean,
    default: true
  }
});
async function openKeepDetails(keepId) {
  // console.log(`[FROM KEEP COMPONENT] payload == ${keepId}`)
  emit('toggle-details', keepId)
}

</script>


<style lang="scss" scoped>
.custom-text-shadow {
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 1.0);
}

.elevation {
  box-shadow: 10px 15px 6px rgba(0, 0, 0, .6);
}

.avatar-elevation {
  box-shadow: 5px 3px 6px rgba(0, 0, 0, .6);
}
</style>