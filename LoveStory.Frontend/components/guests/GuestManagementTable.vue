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
      <tbody
        v-for="({ isExpanded, ...guest }, index) in combinedData"
        :key="index"
      >
        <tr
          class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
        >
          <td>
            <div
              @click="handleExpandDetail({ ...guest, isExpanded })"
              :class="[
                'w-1/2 mx-auto transition-transform duration-500',
                { 'rotate-90': isExpanded },
              ]"
            >
              <svg
                class="h-8 w-8 text-gray-500"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
              >
                <polyline points="9 18 15 12 9 6" />
              </svg>
            </div>
          </td>
          <th
            scope="row"
            class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white"
          >
            {{ index + 1 }}
          </th>
          <td class="px-6 py-4">{{ guest.guestName }}</td>
          <td class="px-6 py-4">{{ guest.guestRelationship }}</td>
          <td class="px-6 py-4">{{ guest.details.length }}</td>
          <td class="px-6 py-4">{{ aggregateSpecialNeedsForGuest(guest) }}</td>
          <td class="px-6 py-4">
            {{ dayjs(guest.createAt).format("YYYY-MM-DD HH:mm") }}
          </td>
          <td class="px-6 py-4">{{ guest.creator.username }}</td>
          <td class="px-6 py-4 text-center">
            <button
              type="button"
              class="px-4 py-2 m-1 rounded-md border border-1 border-transparent bg-pink-300 text-white hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white transition duration-150 ease-in-out"
              @click="emits('request:modify', guest)"
            >
              修改
            </button>
            <button
              type="button"
              class="px-4 py-2 mx-1 rounded-md border border-1 border-red-500 text-red-500 hover:bg-red-100 focus:ring-red-400 active:bg-red-500 active:text-white transition duration-150 ease-in-out"
              @click="emits('request:delete', guest)"
            >
              刪除
            </button>
          </td>
        </tr>
        <tr class="">
          <td :colspan="headerColumns.length" class="p-0">
            <GuestsGuestManagementRowDetail
              :guests="guest.details"
              :class="isExpanded ? 'block' : 'hidden'"
            />
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import dayjs from "dayjs";
type GuestManagementTableProps = { guestManagements: GuestManagement[] };
export type GuestManagementTableData = GuestManagement & {
  isExpanded: boolean;
};

const props = defineProps<GuestManagementTableProps>();
const emits = defineEmits([
  "update:guests",
  "request:modify",
  "request:delete",
]);

const expandStatusRecord = ref<Record<string, boolean>>({});

const combinedData = computed<GuestManagementTableData[]>(() =>
  props.guestManagements.map<GuestManagementTableData>((guest) => ({
    ...guest,
    isExpanded: !!expandStatusRecord.value[guest.guestId],
  }))
);

const handleExpandDetail = (guest: GuestManagementTableData): void => {
  expandStatusRecord.value[guest.guestId] =
    !expandStatusRecord.value[guest.guestId];
};

const headerColumns: string[] = [
  "",
  "#",
  "賓客名字",
  "賓客關係",
  "人數",
  "特殊需求",
  "建立時間",
  "建立者",
  "",
];

const unionForSpecialNeeds = (guestManagement: GuestManagement): string[] => {
  const result = guestManagement.details
    .flatMap((x) => x.specialNeeds)
    .map((x) => x.specialNeedContent);
  return [...new Set<string>(result)];
};

const aggregateSpecialNeedsForGuest = (guest: GuestManagement): string => {
  return unionForSpecialNeeds(guest).join(",");
};
</script>
