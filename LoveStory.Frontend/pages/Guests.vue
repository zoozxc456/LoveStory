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
        @request:modify="handlers.onRequestModifyGuest"
        @request:delete="handlers.onReuestDeleteGuest"
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

    <GuestsDeleteGuestDialog
      v-model:display-controller="
        dialogDisplayControllers['delete-guest-dialog']
      "
      @deleted="onDeletedGuest"
    />

    <GuestsModifyGuestDialog
      v-model:display-controller="
        dialogDisplayControllers['modify-guest-dialog']
      "
      :guest-type="toBeModifiedGuestType"
      @modified="onModifiedGuest"
    />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { useModifySingleGuestStore } from "stores/useModifyGuest";
import { useModifyFamilyGuestStore } from "stores/useModifyFamilyGuest";
import { useDeleteGuestStore } from "stores/useDeleteGuest";

// import {
//   definePageMeta,
//   reactive,
//   ref,
//   provide,
//   useGuestManagement,
//   useDisplayController,
//   type IDisplayController,
//   type GuestManagement,
//   type GuestType,
// } from ".nuxt/imports";

definePageMeta({ layout: "admin-layout" });
const { guests, refreshGuests } = useGuestManagement();

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

const handlers = {
  onRequestModifyGuest: (guest: GuestManagement) => {
    if (guest.guestType === "single") {
      useModifySingleGuestStore().convert(guest);
      toBeModifiedGuestType.value = "single";
    } else {
      useModifyFamilyGuestStore().convert(guest);
      toBeModifiedGuestType.value = "family";
    }

    dialogDisplayControllers["modify-guest-dialog"].onShow();
  },
  onReuestDeleteGuest: (guest: GuestManagement) => {
    useDeleteGuestStore().$patch({
      data: { guestId: guest.guestId, guestName: guest.guestName },
    });

    dialogDisplayControllers["delete-guest-dialog"].onShow();
  },
};

provide("request-operator-guest-events", handlers);

const toBeModifiedGuestType = ref<GuestType>("single");

const onCreatedGuest = () => refreshGuests();
const onModifiedGuest = () => refreshGuests();
const onDeletedGuest = () => refreshGuests();
</script>
