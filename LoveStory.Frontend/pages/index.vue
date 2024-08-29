<template>
  <div
    class="w-full grid grid-cols-1 sm:grid-cols-2 xl:grid-cols-3 gap-4 mx-auto"
  >
    <!-- <DashboardCard v-for="card in cards" v-bind="card" /> -->
  </div>
</template>

<script setup lang="ts">
import { definePageMeta } from ".nuxt/imports";

definePageMeta({ layout: "admin-layout" });
</script>
