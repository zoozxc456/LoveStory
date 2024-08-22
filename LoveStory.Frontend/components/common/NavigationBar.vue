<template>
  <div class="bg-pink-100 px-3 relative">
    <div class="relative w-full h-full">
      <div class="py-3 w-full">
        <img :src="LogoImg" alt="Love Story Logo" class="mx-auto" />
      </div>

      <NuxtLink
        v-for="{ to, displayText, icon } in navItems"
        class="cursor-pointer text-rose-300"
        :to="to"
      >
        <div class="flex justify-start items-center gap-3">
          <div class="w-[24px] flex justify-center items-center">
            <font-awesome-icon :icon="['fas', icon]" />
          </div>
          <div class="py-2">
            {{ displayText }}
          </div>
        </div>
      </NuxtLink>

      <div class="absolute bottom-0 w-full py-2">
        <div class="flex justify-start items-center gap-3 text-rose-900">
          <div class="w-[24px] flex justify-center items-center">
            <font-awesome-icon :icon="['fas', 'arrow-right-from-bracket']" />
          </div>
          <div :class="['py-2 hover:cursor-pointer']" @click="handleLogout">
            Logout
          </div>
        </div>
      </div>
    </div>

    <!-- <div
      :class="[
        'px-3 absolute left-0 bottom-0 w-full h-14 flex justify-start items-center border-t-2 border-gray-100',
        'hover:cursor-pointer hover:bg-rose-50',
      ]"
      class=""
      @click="handleLogout"
    >
      Logout
    </div> -->
  </div>
</template>

<script setup lang="ts">
import LogoImg from "assets/images/Logo.png";

type NameItemType = {
  to: string;
  displayText: string;
  icon: string;
};

const navItems = reactive<NameItemType[]>([
  { to: "/", displayText: "總覽", icon: "house-chimney-window" },
  { to: "/gifts", displayText: "禮金管理", icon: "gift" },
  { to: "/guests", displayText: "賓客名單", icon: "people-group" },
]);

const handleLogout = () => {
  removeAccessToken();
  navigateTo("/login");
};
</script>
