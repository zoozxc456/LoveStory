<template>
  <div>
    <div class="h-full w-full relative">
      <div class="w-full h-full min-h-[75px]">
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
        class="w-full h-[215px] bg-pink-100/80 transition-all duration-1000 -z-[1] absolute p-3 text-rose-300"
        :class="[isExpandedMenu ? 'top-[75px]' : '-top-[215px]']"
        @click="handleClickNavigator"
      >
        <NuxtLink
          v-for="({ to, displayText, icon }, index) in paths"
          class="cursor-pointer"
          :to="to"
          :key="index"
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
  </div>
</template>

<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { ref, useNavigator } from ".nuxt/imports";
const isExpandedMenu = ref<boolean>(false);

const { paths, handleLogout } = useNavigator();

const handleCollsapeMenu = () => (isExpandedMenu.value = !isExpandedMenu.value);

const handleClickNavigator = () => (isExpandedMenu.value = false);
</script>
