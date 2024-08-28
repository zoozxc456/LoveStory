<template>
  <div class="p-3 w-80 h-36 rounded-md border-2 border-pink-100 flex flex-col">
    <div class="text-center basis-1/5 header">{{ props.header }}</div>
    <div class="text-center basis-4/5 flex flex-col justify-center body">
      <div class="w-full" v-for="text in props.body">{{ text }}</div>
    </div>
  </div>
</template>

<script setup lang="ts">
export type DashboardCardProps = {
  header: string;
  body: string[];
};

const props = defineProps<DashboardCardProps>();
</script>
