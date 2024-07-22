<template>
  <div class="h-screen flex gap-2">
    <NavigationBar class="w-1/5 h-full" />
    <slot name="content" class="w-4/5"></slot>
  </div>
</template>

<script setup lang="ts">
import NavigationBar from "../components/common/NavigationBar.vue";
</script>
