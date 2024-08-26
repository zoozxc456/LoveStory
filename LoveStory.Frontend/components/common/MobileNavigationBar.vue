<template>
  <div class="relative w-full">
    <div class="relative w-full h-full">
      <div class="w-full h-full bg-pink-100 flex justify-center items-center">
        <p class="font-millerstone text-2xl text-center w-full">Love Story</p>
      </div>
      <button
        class="absolute top-1/2 right-6 -translate-y-1/2"
        @click="handleCollsapeMenu"
      >
        <font-awesome-icon
          :icon="['fas', 'bars']"
          size="1x"
          class="text-white"
        />
      </button>
    </div>
    <div
      class="w-full bg-pink-100/80 transition-all duration-1000 -z-10 absolute p-3 text-rose-300"
      :class="[isExpandedMenu ? 'top-[100%]' : '-top-[200%]']"
    >
      <NuxtLink
        v-for="{ to, displayText, icon } in navItems"
        class="cursor-pointer"
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

      <div class="border border-white"></div>

      <div class="flex justify-start items-center gap-3">
        <div class="w-[24px] flex justify-center items-center">
          <font-awesome-icon :icon="['fas', 'arrow-right-from-bracket']" />
        </div>
        <div
          :class="['py-2', 'hover:cursor-pointer hover:bg-rose-50']"
          @click="handleLogout"
        >
          Logout
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
const isExpandedMenu = ref<boolean>(false);

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

const handleCollsapeMenu = () => (isExpandedMenu.value = !isExpandedMenu.value);
</script>
