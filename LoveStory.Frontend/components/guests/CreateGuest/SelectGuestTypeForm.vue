<template>
  <div
    class="mx-auto flex w-4/5 max-w-[36rem] flex-col rounded-xl bg-white bg-clip-border text-gray-700 shadow-md"
    :class="['absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2']"
  >
    <div class="flex flex-col gap-4 p-6 relative">
      <h4
        class="block font-sans text-2xl text-center antialiased font-semibold leading-snug tracking-normal text-blue-gray-900"
      >
        請選擇新賓客類型
      </h4>
      <div
        class="w-16 h-16 absolute right-0 top-1/2 -translate-y-1/2 flex justify-center items-center cursor-pointer"
        @click.prevent="emits('cancel')"
      >
        X
      </div>
    </div>
    <div class="flex justify-around items-center pb-6">
      <div
        class="flex justify-center items-center text-2xl w-32 h-32 border-solid border-pink-300 border-2 rounded-lg cursor-pointer"
        @click.prevent="model = 'single'"
      >
        先生/小姐
      </div>
      <div
        class="flex justify-center items-center text-2xl w-32 h-32 border-solid border-pink-300 border-2 rounded-lg cursor-pointer"
        @click.prevent="model = 'family'"
      >
        全家福
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import type { GuestType } from ".nuxt/imports";
const model = defineModel<GuestType | null>({ required: true });
const emits = defineEmits<{ cancel: [] }>();
</script>
