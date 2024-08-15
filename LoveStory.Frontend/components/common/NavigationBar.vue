<template>
  <div class="bg-pink-100 px-3 relative">
    <div class="relative w-full h-full">
      <div class="py-3 w-full">
        <img :src="LogoImg" alt="Love Story Logo" class="mx-auto" />
      </div>

      <NuxtLink v-for="{ to, displayText } in navItems" :to="to">
        <div class="py-3 w-full">{{ displayText }}</div>
      </NuxtLink>
    </div>
    <div
      :class="[
        'px-3 absolute left-0 bottom-0 w-full h-14 flex justify-start items-center border-t-2 border-gray-100',
        'hover:cursor-pointer hover:bg-rose-50',
      ]"
      class=""
      @click="handleLogout"
    >
      Logout
    </div>
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

const handleLogout = () => {
  removeAccessToken();
  navigateTo("/login");
};
</script>
