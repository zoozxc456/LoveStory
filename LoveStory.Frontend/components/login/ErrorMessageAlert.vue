<template>
  <div
    class="h-dvh w-dvw absolute top-0 z-[9999] bg-black/80"
    v-if="displayController.state.isShow"
  >
    <div class="relative w-full h-full">
      <div
        class="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-[80%] h-[200px] border border-slate-100 rounded-md bg-rose-100 text-red-700 text-center font-bold leading-relaxed"
      >
        <div class="relative w-full h-full flex flex-col justify-center">
          <div
            class="absolute top-4 right-6 hover:cursor-pointer"
            @click="displayController.onClose"
          >
            <font-awesome-icon
              :icon="['fas', 'xmark']"
              size="1x"
              class="text-white"
            />
          </div>
          <p class="text-sm sm:text-xl md:text-2xl">
            Your Username or Password is Incorrect.
          </p>
          <p class="text-sm sm:text-xl md:text-2xl">Please Try It Again.</p>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import type { IDisplayController } from ".nuxt/imports";
const displayController = defineModel<IDisplayController>("displayController", {
  required: true,
});
</script>
