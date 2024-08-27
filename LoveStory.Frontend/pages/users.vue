<template>
  <div class="w-full h-full mx-auto">
    <div
      class="header h-[10%] flex justify-center items-center text-2xl relative"
    >
      <h1>帳號管理</h1>
      <button
        :class="[
          'hidden select-none rounded-md border border-transparent bg-pink-300 text-white py-2 px-6 text-center align-middle font-sans text-sm font-bold uppercases',
          'hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 hover:shadow-lg',
          'active:bg-pink-600 active:text-white active:opacity-[0.85]',
          'focus:ring-pink-400 focus:opacity-[0.85] focus:shadow-none ',
          'duration-150 ease-in-out transition-all',
          'disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none',
          'absolute bottom-2 right-4',
          'lg:block',
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
        @request:modify-basic-info="handlers.onRequestModifyUserBasicInfo"
        @request:modify-password-status="handlers.onRequestModifyPasswordStatus"
        @request:delete="handlers.onRequestDeleteUser"
      />

      <ManagementUsersMobileUserManagementTable
        @request:modify-basic-info="handlers.onRequestModifyUserBasicInfo"
        @request:modify-password-status="handlers.onRequestModifyPasswordStatus"
        @request:delete="handlers.onRequestDeleteUser"
      />

      <button
        :class="[
          'block select-none rounded-full border border-transparent bg-pink-300 text-white p-3 text-center align-middle font-sans text-sm font-bold uppercases',
          'hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 hover:shadow-lg',
          'active:bg-pink-600 active:text-white active:opacity-[0.85]',
          'focus:ring-pink-400 focus:opacity-[0.85] focus:shadow-none ',
          'duration-150 ease-in-out transition-all',
          'disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none',
          'absolute bottom-0 right-2',
          'lg:hidden',
        ]"
        type="button"
        @click="dialogDisplayControllers['create-user-dialog'].onShow"
      >
        <FontAwesomeIcon :icon="['fas', 'user-plus']" size="1x" />
      </button>
    </div>

    <ManagementUsersCreateUserDialog
      v-model:display-controller="
        dialogDisplayControllers['create-user-dialog']
      "
      @created="onCreatedUser"
    />

    <ManagementUsersModifyUserBasicInfoDialog
      v-model:data="toBeModifiedBasicInfoUser"
      v-model:display-controller="
        dialogDisplayControllers['modify-user-basic-info-dialog']
      "
      @modified="onModifiedUserBasicInfo"
    />

    <ManagementUsersModifyPasswordStatusDialog
      v-model:display-controller="
        dialogDisplayControllers['modify-user-password-status-dialog']
      "
      :data="tobeModifiedPasswordStatusUser"
      @modified="onModifiedPasswordStatus"
    />

    <ManagementUsersDeleteUserDialog
      v-model:display-controller="
        dialogDisplayControllers['delete-user-dialog']
      "
      :data="toBeDeletedUser"
      @deleted="onDeletedUser"
    />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { useUserManagementStore } from "../stores/management/users/useUserManagement";

definePageMeta({ layout: "admin-layout" });
const store = useUserManagementStore();
store.fetchAllUsers();

const dialogDisplayControllers = reactive<{
  [key in
    | "create-user-dialog"
    | "modify-user-basic-info-dialog"
    | "modify-user-password-status-dialog"
    | "delete-user-dialog"]: IDisplayController;
}>({
  "create-user-dialog": useDisplayController(),
  "modify-user-basic-info-dialog": useDisplayController(),
  "modify-user-password-status-dialog": useDisplayController(),
  "delete-user-dialog": useDisplayController(),
});

const toBeModifiedBasicInfoUser = reactive<
  Pick<UserManagement, "userId" | "username" | "role">
>({
  username: "",
  role: "",
  userId: "",
});

const tobeModifiedPasswordStatusUser = reactive<
  Pick<UserManagement, "userId" | "username">
>({
  username: "",
  userId: "",
});

const toBeDeletedUser = reactive<Pick<UserManagement, "userId" | "username">>({
  username: "",
  userId: "",
});

const handlers = {
  onRequestModifyUserBasicInfo: ({
    userId,
    username,
    role,
  }: UserManagement) => {
    toBeModifiedBasicInfoUser.userId = userId;
    toBeModifiedBasicInfoUser.username = username;
    toBeModifiedBasicInfoUser.role = role;

    dialogDisplayControllers["modify-user-basic-info-dialog"].onShow();
  },

  onRequestModifyPasswordStatus: ({ userId, username }: UserManagement) => {
    tobeModifiedPasswordStatusUser.userId = userId;
    tobeModifiedPasswordStatusUser.username = username;

    dialogDisplayControllers["modify-user-password-status-dialog"].onShow();
  },
  onRequestDeleteUser: ({ userId, username }: UserManagement) => {
    toBeDeletedUser.userId = userId;
    toBeDeletedUser.username = username;

    dialogDisplayControllers["delete-user-dialog"].onShow();
  },
};

const onCreatedUser = () => {
  store.refreshUsers();
};
const onModifiedUserBasicInfo = () => store.refreshUsers();
const onModifiedPasswordStatus = () => store.refreshUsers();
const onDeletedUser = () => store.refreshUsers();

watchEffect(() => {
  console.log(`===== Start Console.log for user =====`);
  console.log(store.users);
  console.log(`===== End Console.log for user =====`);
});
</script>
