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
          賓客名字
        </div>
        <div
          class="flex-[4] text-center text-xs text-gray-700 uppercase font-bold py-3"
        >
          賓客關係
        </div>
      </div>
      <div class="body w-full relative">
        <div
          class="row w-full border-b"
          v-for="(guest, index) in combinedData"
          :key="guest.guestId"
        >
          <div class="presentation w-full flex relative items-center">
            <div
              class="absolute left-[15px] top-1/2 -translate-y-1/2 hover:cursor-pointer"
              @click="handleExpandGuestInfo(guest)"
            >
              <font-awesome-icon :icon="['fas', 'angle-right']" size="1x" />
            </div>
            <div class="flex-1 text-center text-xs text-gray-700 px-6 py-3">
              {{ index + 1 }}
            </div>
            <div class="flex-[4] text-center text-xs text-gray-700 py-3">
              {{ guest.guestName }}
            </div>
            <div class="flex-[4] text-center text-xs text-gray-700 py-3">
              {{ guest.guestRelationship }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <GuestsDialogsMobilePreview
    v-model:controller="previewController"
    v-model:guest="toBePreviewedGuest"
  />
</template>

<script setup lang="ts">
const previewController = useDisplayController();

const toBePreviewedGuest = ref<GuestManagementTableData>({
  guestId: "",
  guestRelationship: "",
  createAt: new Date(),
  creator: {
    userId: "",
    username: "",
  },
  guestName: "",
  guestType: "single",
  details: [],
  isExpanded: false,
});

type GuestManagementTableProps = { guestManagements: GuestManagement[] };
export type GuestManagementTableData = GuestManagement & {
  isExpanded: boolean;
};
const props = defineProps<GuestManagementTableProps>();

const expandStatusRecord = ref<Record<string, boolean>>({});

const combinedData = computed<GuestManagementTableData[]>(() =>
  props.guestManagements.map<GuestManagementTableData>((guest) => ({
    ...guest,
    isExpanded: !!expandStatusRecord.value[guest.guestId],
  }))
);

const handleExpandGuestInfo = (guest: GuestManagementTableData) => {
  toBePreviewedGuest.value = guest;
  previewController.onShow();
};
</script>
