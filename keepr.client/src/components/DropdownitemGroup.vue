<template>
  <!-- Menu Label / START -->
  <div :class="['px-1', props.label ? 'pt-5 pb-1' : 'py-1']">
    <div v-if="props.label" class="mb-1.5 px-2 text-[14px] font-bold uppercase tracking-tight">
      {{ props.label }}
    </div>
    <!-- MENU LABEL / END -->
    <slot />
    <!-- MENU ITEM SLOT -->
  </div>
</template>




<script setup>

const props = defineProps({
  label: String
})
</script>


<style lang="scss" scoped></style>