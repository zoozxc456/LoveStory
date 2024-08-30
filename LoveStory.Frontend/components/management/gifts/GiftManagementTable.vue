<template>
  <div
    class="relative shadow-md sm:rounded-lg h-full hidden lg:block overflow-y-auto"
  >
    <table
      class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400"
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
      <tbody
        v-for="(management, index) in store.data"
        :key="management.managementId"
      >
        <tr
          class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
        >
          <td class="text-center">
            <div
              :class="[
                'w-1/2 mx-auto transition-transform duration-500 cursor-pointer',
                store.hasAnyWeddingGift(management.attendance)
                  ? 'block'
                  : 'hidden',
                expandedRecordSet.has(management.managementId)
                  ? 'rotate-90'
                  : '',
              ]"
              @click="
                handlers.onToggleWeddingGiftMoreInfo({
                  managementId: management.managementId,
                })
              "
            >
              <FontAwesomeIcon :icon="['fas', 'angle-right']" size="1x" />
            </div>
          </td>
          <th
            scope="row"
            class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"
          >
            {{ index + 1 }}
          </th>
          <td class="px-6 py-4">{{ management.managementName }}</td>
          <td class="px-6 py-4">
            {{ management.attendance[0].guestRelationship }}
            <span>
              ({{
                management.managementType === "single" ? "先生/小姐" : "全家福"
              }})</span
            >
          </td>
          <td class="px-6 py-4">{{ store.aggregateGifts(management) }}</td>
          <td
            v-if="management.attendance.some((x) => x.weddingGift)"
            class="px-6 py-4"
          >
            <div class="flex justify-between">
              <button
                type="button"
                class="px-4 py-2 rounded-md border border-pink-400 text-pink-400 bg-transparent hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white transition duration-150 ease-in-out"
                @click="emits('request:modify', management)"
              >
                修改
              </button>
              <button
                type="button"
                class="px-4 py-2 rounded-md border border-red-500 text-red-500 hover:bg-red-100 focus:ring-red-400 active:bg-red-500 active:text-white transition duration-150 ease-in-out"
                @click="emits('request:delete', management)"
              >
                刪除
              </button>
            </div>
          </td>
          <td v-else class="px-6 py-4">
            <button
              type="button"
              class="px-4 py-2 w-full rounded-md border border-transparent bg-pink-300 text-white hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white transition duration-150 ease-in-out"
              @click="emits('request:create', management)"
            >
              新增
            </button>
          </td>
        </tr>
        <tr
          v-if="store.hasAnyWeddingGift(management.attendance)"
          :class="[
            expandedRecordSet.has(management.managementId) ? '' : 'hidden',
          ]"
        >
          <td :colspan="headerColumns.length">
            <div class="grid grid-flow-col h-52">
              <div
                class="h-5/6 mx-6 my-auto border-gray-400 border rounded-md"
                v-for="{
                  guestName,
                  weddingGift,
                  guestId,
                } in management.attendance"
                :key="guestId"
              >
                <div
                  class="h-1/4 text-center flex justify-center items-center guest-name"
                >
                  {{ guestName }}
                </div>
                <div class="h-3/4 w-full text-center">
                  <div class="grid grid-cols-2 py-1 special-needs">
                    <div class="label">禮金</div>
                    <div class="content">
                      {{ store.presentationGift(weddingGift) }}
                    </div>
                  </div>
                  <div class="grid grid-cols-2 py-1 remark">
                    <div class="label">備註</div>
                    <div class="content">{{ weddingGift?.remark }}</div>
                  </div>
                  <div class="grid grid-cols-2 py-1 remark">
                    <div class="label">收禮時間</div>
                    <div class="content">
                      {{ store.presentationReceivedAt(weddingGift) }}
                    </div>
                  </div>
                  <div class="grid grid-cols-2 py-1 remark">
                    <div class="label">收禮人</div>
                    <div class="content">
                      {{ weddingGift?.recepient.username }}
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { useWeddingGiftManagementStore } from "stores/management/wedding-gifts/useWeddingGiftManagement";

const emits = defineEmits<{
  (e: "request:create", data: IWeddingGiftManagement): void;
  (e: "request:delete", data: IWeddingGiftManagement): void;
  (e: "request:modify", data: IWeddingGiftManagement): void;
}>();

const headerColumns: string[] = [
  "",
  "#",
  "賓客名稱",
  "賓客關係",
  "禮金金額",
  "",
];

const store = useWeddingGiftManagementStore();
store.fetch();

const expandedRecordSet = ref<Set<string>>(new Set<string>());

const handlers = {
  onToggleWeddingGiftMoreInfo: ({
    managementId,
  }: Pick<IWeddingGiftManagement, "managementId">) => {
    if (expandedRecordSet.value.has(managementId))
      expandedRecordSet.value.delete(managementId);
    else expandedRecordSet.value.add(managementId);
  },
};
</script>
