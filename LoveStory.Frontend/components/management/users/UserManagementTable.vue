<template>
  <div
    class="relative shadow-md sm:rounded-lg h-full hidden lg:block overflow-y-auto"
  >
    <table
      class="w-full max-h-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400 overflow-y-scroll"
    >
      <thead
        class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400"
      >
        <tr>
          <th
            scope="col"
            class="px-6 py-3"
            v-for="(column, index) in headerColumns"
            :key="index"
          >
            {{ column }}
          </th>
        </tr>
      </thead>
      <tbody v-for="(user, index) in store.users" :key="user.userId">
        <tr
          class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
        >
          <th
            scope="row"
            class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"
          >
            {{ index + 1 }}
          </th>
          <td class="px-6 py-4">{{ user.username }}</td>
          <td class="px-6 py-4">{{ user.role }}</td>
          <td class="px-6 py-4">
            {{ dayjs(user.createAt).format("YYYY-MM-DD HH:mm") }}
          </td>
          <td class="px-6 py-4">{{ user.creator?.username }}</td>
          <td class="px-6 py-4 text-center">
            <button
              type="button"
              class="px-4 py-2 m-1 rounded-md border border-1 border-transparent bg-pink-300 text-white hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white transition duration-150 ease-in-out"
              @click="emits('request:modify-password-status', user)"
            >
              重設密碼
            </button>
            <button
              type="button"
              class="px-4 py-2 m-1 rounded-md border border-1 border-transparent bg-pink-300 text-white hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white transition duration-150 ease-in-out"
              @click="emits('request:modify-basic-info', user)"
            >
              修改
            </button>
            <button
              type="button"
              class="px-4 py-2 mx-1 rounded-md border border-1 border-red-500 text-red-500 hover:bg-red-100 focus:ring-red-400 active:bg-red-500 active:text-white transition duration-150 ease-in-out"
              @click="emits('request:delete', user)"
            >
              刪除
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import dayjs from "dayjs";
// import type { UserManagement } from ".nuxt/imports";
import { useUserManagementStore } from "stores/management/users/useUserManagement";

const store = useUserManagementStore();
const emits = defineEmits<{
  (e: "request:modify-basic-info", user: UserManagement): void;
  (e: "request:modify-password-status", user: UserManagement): void;
  (e: "request:delete", user: UserManagement): void;
}>();

const headerColumns: string[] = [
  "#",
  "使用者名稱",
  "角色權限",
  "建立時間",
  "建立者",
  "",
];
</script>
