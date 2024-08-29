<template>
  <div class="w-full h-full mx-auto">
    <div
      class="header h-[10%] flex justify-center items-center text-2xl relative"
    >
      <h1>禮金管理</h1>
      <button
        :class="[
          'select-none rounded-md border border-transparent bg-pink-300 text-white py-2 px-6 text-center align-middle font-sans text-sm font-bold uppercases',
          'hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 hover:shadow-lg',
          'active:bg-pink-600 active:text-white active:opacity-[0.85]',
          'focus:ring-pink-400 focus:opacity-[0.85] focus:shadow-none ',
          'duration-150 ease-in-out transition-all',
          'disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none',
          'absolute bottom-2 right-4',
        ]"
        type="button"
        @click="dialogDisplayControllers['create-wedding-gift-dialog'].onShow"
      >
        新增禮金
      </button>
    </div>
    <div class="content h-[89%] max-h-[89%] mx-4">
      <ManagementGiftsGiftManagementTable />
    </div>
  </div>
</template>

<script setup lang="ts">
definePageMeta({ layout: "admin-layout" });
const dialogDisplayControllers = reactive<{
  [key in
    | "create-wedding-gift-dialog"
    | "modify-wedding-gift-dialog"
    | "delete-wedding-gift-dialog"]: IDisplayController;
}>({
  "create-wedding-gift-dialog": useDisplayController(),
  "modify-wedding-gift-dialog": useDisplayController(),
  "delete-wedding-gift-dialog": useDisplayController(),
});
</script>
