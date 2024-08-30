<template>
  <div class="relative h-11 w-full min-w-[200px]">
    <div ref="dropDownListElementRef">
      <slot name="presentation"></slot>
      <slot name="list"></slot>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
// import {
//   ref,
//   onMounted,
//   onBeforeUnmount,
//   type IDisplayController,
// } from ".nuxt/imports";

const displayController = defineModel<IDisplayController>("displayController", {
  required: true,
});

const dropDownListElementRef = ref<HTMLDivElement | null>(null);

const handleClickOutside = ({ target }: MouseEvent) => {
  if (
    dropDownListElementRef.value &&
    !dropDownListElementRef.value.contains(target as HTMLElement)
  ) {
    displayController.value.onClose();
  }
};

onMounted(() => {
  document.addEventListener("click", handleClickOutside);
});

onBeforeUnmount(() => {
  document.removeEventListener("click", handleClickOutside);
});
</script>
