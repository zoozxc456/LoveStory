<template>
  <div class="w-full grid grid-cols-1 sm:grid-cols-2 xl:grid-cols-3 gap-4 mx-auto">
    <DashboardCard v-for="card in cards" v-bind="card" />
  </div>
</template>

<script setup lang="ts">
type Card = {
  header: string;
  body: string[];
};

import { ref } from "vue";
import DashboardCard from "../components/index/DashboardCard.vue";

const cards = ref<Card[]>([
  { header: "已邀請", body: ["352 位賓客", "(男方: 100 位 / 女方: 100 位)"] },
  { header: "已簽到賓客", body: ["72 / 100"] },
]);
</script>
