<template>
  <MenuItem v-slot="{ active }">
  <component :is="props.as" :class="[
    active ? 'bg-violet-500 text-white' : 'text-gray-900',
    'group flex w-full flex-col items-start rounded-md px-2 py-2 text-sm',
  ]">

    <span>
      <slot />
    </span>
    <span v-if="props.description"
      class="mt-0.5 text-xs text-gray-500 group-hover:text-white">{{ $props.description }}</span>
  </component>
  </MenuItem>
</template>


<script setup>
import { MenuItem } from '@headlessui/vue'
const props = defineProps({
  as: {
    type: String,
    default: "a",
  },
  description: String,
});
</script>


<style lang="scss" scoped></style>