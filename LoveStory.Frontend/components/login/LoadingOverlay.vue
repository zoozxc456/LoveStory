<template>
  <div
    class="h-dvh w-dvw absolute top-0 z-[9999] bg-black/80"
    v-if="props.isLoading"
  >
    <div class="relative w-full h-full">
      <div class="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2">
        <span class="text-white text-2xl font-bold"> Loading ... </span>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
type LoadingProps = { isLoading: boolean };

const props = defineProps<LoadingProps>();
</script>
