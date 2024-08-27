<template>
  <div class="w-full h-full mx-auto">
    <div
      class="header h-[10%] flex justify-center items-center text-2xl relative"
    >
      <h1>帳號管理</h1>
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
        @click="dialogDisplayControllers['create-user-dialog'].onShow"
      >
        新增使用者
      </button>
    </div>
    <div class="content h-[89%] max-h-[89%] mx-4">
      <ManagementUsersUserManagementTable
        :user-managements="store.users"
        @request:modify="handlers.onRequestModifyUserBasicInfo"
      />
      <!-- <GuestsGuestManagementTable
        :guest-managements="guests"
        @update:guests="refreshGuests"
        @request:modify="handlers.onRequestModifyGuest"
        @request:delete="handlers.onReuestDeleteGuest"
      />
      <GuestsMobileGuestManagementTable
        :guest-managements="guests"
        @update:guests="refreshGuests"
      /> -->
    </div>

    <ManagementUsersCreateUserDialog
      v-model:display-controller="
        dialogDisplayControllers['create-user-dialog']
      "
      @created="onCreatedUser"
    />

    <ManagementUsersModifyUserBasicInfoDialog
      v-model:data="toBeModifiedUser"
      v-model:display-controller="
        dialogDisplayControllers['modify-user-dialog']
      "
      @modified="onModifiedUser"
    />
    <!-- <GuestsCreateGuestDialog
      v-model:display-controller="
        dialogDisplayControllers['create-user-dialog']
      "
      @created="onCreatedGuest"
    />

    <GuestsDeleteGuestDialog
      v-model:display-controller="
        dialogDisplayControllers['delete-user-dialog']
      "
      @deleted="onDeletedGuest"
    />

    <GuestsModifyGuestDialog
      v-model:display-controller="
        dialogDisplayControllers['modify-user-dialog']
      "
      :guest-type="toBeModifiedGuestType"
      @modified="onModifiedGuest"
    /> -->
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { useUserManagementStore } from "../stores/management/users/useUserManagement";

definePageMeta({ layout: "admin-layout" });
const store = useUserManagementStore();
store.fetchAllUsers();

const dialogDisplayControllers = reactive<{
  [key in
    | "create-user-dialog"
    | "modify-user-dialog"
    | "delete-user-dialog"]: IDisplayController;
}>({
  "create-user-dialog": useDisplayController(),
  "modify-user-dialog": useDisplayController(),
  "delete-user-dialog": useDisplayController(),
});

const toBeModifiedUser = reactive<
  Pick<UserManagement, "userId" | "username" | "role">
>({
  username: "",
  role: "",
  userId: "",
});

const handlers = {
  onRequestModifyUserBasicInfo: ({
    userId,
    username,
    role,
  }: UserManagement) => {
    Object.assign(toBeModifiedUser, { userId, username, role });
    dialogDisplayControllers["modify-user-dialog"].onShow();
  },
};
//   onReuestDeleteGuest: (guest: GuestManagement) => {
//     useDeleteGuestStore().$patch({
//       data: { guestId: guest.guestId, guestName: guest.guestName },
//     });

//     dialogDisplayControllers["delete-guest-dialog"].onShow();
//   },
// };

// provide("request-operator-guest-events", handlers);

const toBeModifiedGuestType = ref<GuestType>("single");

const onCreatedUser = () => {
  store.refreshUsers();
};
const onModifiedUser = () => {
  store.refreshUsers();
};
const onDeletedUser = () => {};

watchEffect(() => {
  console.log(`===== Start Console.log for user =====`);
  console.log(store.users);
  console.log(`===== End Console.log for user =====`);
});
</script>
