<template>
  <div class="relative shadow-md sm:rounded-lg mx-4 max-h-[95vh]">
    <div class="absolute -top-12 right-0">
      <button
        class="select-none rounded-md border border-1 border-transparent bg-pink-300 text-white hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white duration-150 ease-in-out py-2 px-6 text-center align-middle font-sans text-sm font-bold uppercases hadow-md transition-all hover:shadow-lg focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
        type="button"
        @click="createGuestDialogDisplayController.onShow"
      >
        新增賓客
      </button>
    </div>
    <table
      class="w-full max-h-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400 overflow-y-scroll"
    >
      <thead
        class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400"
      >
        <tr>
          <th scope="col" class="px-6 py-3" v-for="column in headerColumns">
            {{ column }}
          </th>
        </tr>
      </thead>
      <tbody v-for="({ isExpanded, ...guest }, index) in combinedData">
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
          <td class="px-6 py-4">
            <button
              type="button"
              class="px-4 py-2 mx-1 rounded-md border border-1 border-transparent bg-pink-300 text-white hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white transition duration-150 ease-in-out"
            >
              修改
            </button>
            <button
              type="button"
              class="px-4 py-2 mx-1 rounded-md border border-1 border-red-500 text-red-500 hover:bg-red-100 focus:ring-red-400 active:bg-red-500 active:text-white transition duration-150 ease-in-out"
              @click="handleShowDeleteGuestDialog(guest)"
            >
              刪除
            </button>
          </td>
        </tr>
        <tr class="">
          <td :colspan="headerColumns.length" class="p-0">
            <GuestManagementRowDetail
              :guests="guest.details"
              :class="isExpanded ? 'block' : 'hidden'"
            />
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <CreateGuestDialog
    v-model:controller="createGuestDialogDisplayController"
    @update:guests="emits('update:guests')"
  />

  <DeleteGuestDialog
    v-if="deleteGuestDialogIsShow"
    :guest-id="deleteGuestDialogData.guestId"
    :guest-name="deleteGuestDialogData.guestName"
    @close-dialog="handleCancelDeleteGuestDialog"
    @delete-guest="handleDeleteGuestById"
  />
</template>

<script setup lang="ts">
import dayjs from "dayjs";
import GuestManagementRowDetail from "./GuestManagementRowDetail.vue";
import DeleteGuestDialog from "./DeleteGuestDialog.vue";
import CreateGuestDialog from "./CreateGuestDialog/CreateGuestDialog.vue";
import type { GuestManagement } from "types/GuestManagement/guestManagement.type";
import { useDialogDisplayController } from "../../composables/admin/useDialogDisplayController";
import { useDeleteGuestDialog } from "../../composables/admin/useDeleteGuestDialog";
type GuestManagementTableProps = { guestManagements: GuestManagement[] };
type GuestManagementTableData = GuestManagement & { isExpanded: boolean };
const createGuestDialogDisplayController = useDialogDisplayController();

const props = defineProps<GuestManagementTableProps>();
const emits = defineEmits(["update:guests"]);
const {
  data: deleteGuestDialogData,
  isShow: deleteGuestDialogIsShow,
  handleTriggerShowDialog: handleShowDeleteGuestDialog,
  handleCancel: handleCancelDeleteGuestDialog,
  handleDeleteGuestById,
} = useDeleteGuestDialog(useDialogDisplayController(), emits);

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
