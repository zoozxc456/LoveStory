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
const displayController = defineModel<IDialogDisplayController>(
  "displayController",
  { default: {} }
);

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
