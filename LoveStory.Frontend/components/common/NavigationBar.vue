<template>
  <div class="bg-pink-100 divide-y px-3">
    <div class="py-3">
      <img :src="LogoImg" alt="Love Story Logo" class="mx-auto" />
    </div>

    <NuxtLink v-for="{ to, displayText } in navItems" :to="to">
      <div class="py-3">{{ displayText }}</div>
    </NuxtLink>
  </div>
</template>

<script setup lang="ts">
import LogoImg from "assets/images/Logo.png";

type NameItemType = {
  to: string;
  displayText: string;
};

const navItems = reactive<NameItemType[]>([
  { to: "/", displayText: "總覽" },
  { to: "/gifts", displayText: "禮金管理" },
  { to: "/guests", displayText: "賓客名單" },
]);
</script>
