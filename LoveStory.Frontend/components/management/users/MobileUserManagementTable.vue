<template>
  <div class="relative shadow-md sm:rounded-lg h-full block lg:hidden">
    <div class="w-full">
      <div
        class="header w-full text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 flex justify-center items-center"
      >
        <div
          class="flex-1 text-center text-xs text-gray-700 uppercase font-bold px-6 py-3"
        >
          #
        </div>
        <div
          class="flex-[4] text-center text-xs text-gray-700 uppercase font-bold py-3"
        >
          使用者名稱
        </div>
        <div
          class="flex-[4] text-center text-xs text-gray-700 uppercase font-bold py-3"
        >
          角色權限
        </div>
      </div>
      <div class="body w-full relative">
        <div
          class="row w-full border-b"
          v-for="(user, index) in store.data"
          :key="user.userId"
        >
          <div class="presentation w-full flex relative items-center">
            <div
              class="absolute left-[15px] top-1/2 -translate-y-1/2 hover:cursor-pointer"
              :class="[expandedRecordSet.has(user.userId) ? 'rotate-90' : '']"
              @click="handlers.onToggleUserDetailInfo(user)"
            >
              <font-awesome-icon :icon="['fas', 'angle-right']" size="1x" />
            </div>
            <div class="flex-1 text-center text-xs text-gray-700 px-6 py-3">
              {{ index + 1 }}
            </div>
            <div class="flex-[4] text-center text-xs text-gray-700 py-3">
              {{ user.username }}
            </div>
            <div class="flex-[4] text-center text-xs text-gray-700 py-3">
              {{ user.role }}
            </div>
          </div>
          <div
            class="w-full overflow-auto transition-all duration-500"
            :class="[
              expandedRecordSet.has(user.userId) ? 'mini:h-36 h-32' : 'h-0',
            ]"
          >
            <div class="w-full p-3 text-sm text-gray-700 shadow-inner">
              <div class="flex text-center">
                <div class="flex-1">建立時間</div>
                <div class="flex-1">建立者</div>
              </div>
              <div class="flex text-center py-2">
                <div class="flex-1">
                  {{ dayjs(user.createAt).format("YYYY-MM-DD HH:mm") }}
                </div>
                <div class="flex-1">{{ user.creator?.username }}</div>
              </div>
              <div class="flex text-center">
                <div class="flex-1">
                  <button
                    type="button"
                    class="px-4 py-2 w-24 m-1 rounded-md border border-1 border-transparent bg-pink-300 text-white hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white transition duration-150 ease-in-out"
                    :class="['mini:w-16 mini:h-14']"
                    @click="emits('request:modify-basic-info', user)"
                  >
                    修改
                  </button>
                </div>
                <div class="flex-1">
                  <button
                    type="button"
                    class="px-4 py-2 w-24 m-1 rounded-md border border-1 border-pink-400 text-pink-400 bg-transparent hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white transition duration-150 ease-in-out"
                    :class="['mini:w-16 mini:h-14']"
                    @click="emits('request:modify-password-status', user)"
                  >
                    重設密碼
                  </button>
                </div>

                <div class="flex-1">
                  <button
                    type="button"
                    class="px-4 py-2 w-24 m-1 rounded-md border border-1 border-red-500 text-red-500 hover:bg-red-100 focus:ring-red-400 active:bg-red-500 active:text-white transition duration-150 ease-in-out"
                    :class="['mini:w-16 mini:h-14']"
                    @click="emits('request:delete', user)"
                  >
                    刪除
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import dayjs from "dayjs";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { useUserManagementStore } from "stores/management/users/useUserManagement";
// import { ref ,type UserManagement } from ".nuxt/imports";

const store = useUserManagementStore();
const emits = defineEmits<{
  (e: "request:modify-basic-info", user: UserManagement): void;
  (e: "request:modify-password-status", user: UserManagement): void;
  (e: "request:delete", user: UserManagement): void;
}>();

const expandedRecordSet = ref<Set<string>>(new Set<string>());

const handlers = {
  onToggleUserDetailInfo: ({ userId }: UserManagement) => {
    if (expandedRecordSet.value.has(userId)) {
      expandedRecordSet.value.delete(userId);
    } else {
      expandedRecordSet.value.add(userId);
    }
  },
};
</script>
