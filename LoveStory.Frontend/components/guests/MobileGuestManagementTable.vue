<template>
  <div class="relative shadow-md sm:rounded-lg h-full block lg:hidden">
    <div class="absolute -top-12 right-0">
      <button
        class="select-none rounded-md border border-1 border-transparent bg-pink-300 text-white hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white duration-150 ease-in-out py-2 px-6 text-center align-middle font-sans text-sm font-bold uppercases hadow-md transition-all hover:shadow-lg focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
        type="button"
        @click="createGuestDialogDisplayController.onShow"
      >
        新增賓客
      </button>
    </div>

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
        <div class="row w-full border-b" v-for="(guest, index) in combinedData">
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
    v-model:guest="tempGuest"
  />
</template>

<script setup lang="ts">
const previewController = useDisplayController();

const tempGuest = ref<GuestManagementTableData>({
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
const createGuestDialogDisplayController = useDisplayController();
const modifyGuestDialogDisplayController = useDisplayController();
const temp = ref<GuestManagement | null>(null);

const props = defineProps<GuestManagementTableProps>();
const emits = defineEmits(["update:guests"]);

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
  // "人數",
  // "特殊需求",
  // "建立時間",
  // "建立者",
  // "",
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

const handleShowModifyGuestDialog = (guest: GuestManagement) => {
  temp.value = guest;
  modifyGuestDialogDisplayController.onShow();
};

const handleExpandGuestInfo = (guest: GuestManagementTableData) => {
  console.log(`===== Start Console.log for  =====`);
  console.log(guest);
  console.log(`===== End Console.log for  =====`);
  tempGuest.value = guest;
  previewController.onShow();
};
</script>
