<template>
  <div class="w-full h-full mx-auto">
    <div
      class="header h-[10%] flex justify-center items-center text-2xl relative"
    >
      <h1>賓客管理</h1>
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
        @click="dialogDisplayControllers['create-guest-dialog'].onShow"
      >
        新增賓客
      </button>
    </div>
    <div class="content h-[89%] max-h-[89%] mx-4">
      <GuestsGuestManagementTable
        :guest-managements="guests"
        @update:guests="refreshGuests"
      />
      <GuestsMobileGuestManagementTable
        :guest-managements="guests"
        @update:guests="refreshGuests"
      />
    </div>

    <GuestsCreateGuestDialog
      v-model:display-controller="
        dialogDisplayControllers['create-guest-dialog']
      "
      @created="onCreatedGuest"
    />

    <!-- <GuestsDeleteGuestDialog
      v-if="deleteGuestDialogIsShow"
      :guest-id="deleteGuestDialogData.guestId"
      :guest-name="deleteGuestDialogData.guestName"
      @close-dialog="handleCancelDeleteGuestDialog"
      @delete-guest="handleDeleteGuestById"
    /> -->

    <!-- <GuestsModifyGuestDialog
      v-model:display-controller="
        dialogDisplayControllers['modify-guest-dialog']
      "
      :guest-type="toBeModifiedGuestType"
      @modified="onModifiedGuest"
    /> -->
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
definePageMeta({ layout: "admin-layout" });
const { guests, isLoading, refreshGuests } = useGuestManagement();

const dialogDisplayControllers = reactive<{
  [key in
    | "create-guest-dialog"
    | "modify-guest-dialog"
    | "delete-guest-dialog"]: IDisplayController;
}>({
  "create-guest-dialog": useDisplayController(),
  "modify-guest-dialog": useDisplayController(),
  "delete-guest-dialog": useDisplayController(),
});

const onCreatedGuest = () => refreshGuests();
</script>
