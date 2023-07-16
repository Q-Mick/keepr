<template>
<div class="container mx-1 sm:mx-auto">

<div class="w-fit mr-2 sm:mr-0">

  <div class="columns-2 sm:columns-4 gap-2 sm:gap-8 pt-5">
    <Keep v-for="keep in keeps" :key="keep.id" :keep="keep" class="my-5" :style="{ marginTop: keep.id === keeps[0].id ? '0' : 'auto' }" />
  </div>
  
</div>


</div>
</template>

<script>
import { computed, onMounted, watchEffect, ref } from "vue";
import { AppState } from "../AppState.js";
import { AuthService } from '../services/AuthService'
import { logger } from "../utils/Logger.js";
import { keepsService } from '../services/KeepsService.js'
import { accountService } from "../services/AccountService.js";
// import { Keep } from "../models/Keep.js";
import Keep from '../components/Keep.vue';
export default {
  components: {
    Keep,
  },
    setup() {
        onMounted(() => getKeeps());
        async function getKeeps() {
            try {
                await keepsService.getKeeps();
            }
            catch (error) {
                logger.log(error.message);
            }
        }
        return {
            getKeeps,
            keeps: computed(() => AppState.keeps)
        };
    },
    components: { Keep }
}
</script>

<style scoped lang="scss">


</style>
