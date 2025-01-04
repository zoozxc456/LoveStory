<template>
  <div class="w-full h-full mx-auto">
    <div class="header w-full relative max-h-[25%]">
      <h1 class="text-center text-2xl py-6">座位管理</h1>
      
    </div>

    <div
      class="relative shadow-md sm:rounded-lg hidden lg:block overflow-y-auto max-h-[90%] h-[90%]"
    >
      <table
        class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400"
      >
        <thead
          class="text-sm text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400"
        >
          <tr>
            <th scope="col" class="px-6 py-3">桌位</th>
            <th scope="col" class="px-6 py-3">賓客姓名</th>
            <th scope="col" class="px-6 py-3">賓客關係</th>
            <th scope="col" class="px-6 py-3">特殊需求</th>
            <th scope="col" class="px-6 py-3"></th>
          </tr>
        </thead>
        <tbody
          v-for="table in sortedByTableAliasBanquetTables"
          :key="table.banquetTableId"
        >
          <tr>
            <td>
              <div class="px-6 pt-4 font-bold text-lg">
                {{ table.tableAlias }}
              </div>
            </td>
          </tr>
          <tr
            class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
            v-for="guest in matchedGuestsByTable(table)"
            :key="guest.guestId"
          >
            <td></td>
            <td class="px-6 py-2">
              {{ guest.guestName }}
            </td>

            <td class="px-6 py-2">
              {{ guest.guestRelationship }}
            </td>
            <td class="px-6 py-2">
              {{ guest.specialNeeds.join(",") }}
            </td>

            <td class="px-6 py-2">
              <div class="flex justify-between">
                <button
                  type="button"
                  class="px-4 py-2 rounded-md border border-pink-400 text-pink-400 bg-transparent hover:bg-pink-100 hover:text-rose-500 hover:border-pink-200 focus:ring-pink-400 active:bg-pink-600 active:text-white transition duration-150 ease-in-out"
                  @click="handleOpenModifyGuestSeatLocation(guest, table)"
                >
                  修改
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>

  <ManagementSeatLocationModifyGuestSeatLocationDialog
    v-model:display-controller="dialogDisplayController"
    v-model:selected-seat-location-info="selectedGuestSeatLocationInfo"
    :tables="tables"
    @on-modify="handleModifyGuestSeatLocation"
  />
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
definePageMeta({ layout: "admin-layout" });

const state = ref<GuestSeatLocationManagement[]>([]);
const tables = ref<BanquetTableManagement[]>([]);
const dialogDisplayController = useDisplayController();

const fetchData = async () => {
  (async () => {
    const { data } = await useAsyncData<GuestSeatLocationManagement[]>(
      "a",
      () =>
      $fetch("/api/admin/seat-location/guests", {
          method: "GET",
          headers: generateJwtAuthorizeHeader(),
        })
    );

    if (data.value) state.value = data.value;
  })();

  (async () => {
    const { data } = await useAsyncData<IBanquetTable[]>("b", () =>
      $fetch("/api/admin/banquent-tables", {
        method: "GET",
        headers: generateJwtAuthorizeHeader(),
      })
    );
    if (data.value) tables.value = data.value;
  })();
};

fetchData();

const matchedGuestsByTable = ({
  banquetTableId,
  tableAlias,
}: BanquetTableManagement) => {
  let result: GuestSeatLocationManagement[] = [];

  if (banquetTableId === "")
    result = state.value.filter((x) => x.tableAlias === null);
  else result = state.value.filter((x) => x.tableAlias === tableAlias);

  return result.sort((x, y) => {
    const comparison = x.guestRelationship.localeCompare(y.guestRelationship);
    if (comparison !== 0) return comparison;

    return x.guestName.localeCompare(y.guestName);
  });
};

const selectedGuestSeatLocationInfo = ref<{
  guest: GuestSeatLocationManagement;
  selectedSeatLocation: BanquetTableManagement;
}>({
  guest: {
    guestId: "",
    guestName: "",
    guestRelationship: "",
    tableAlias: undefined,
    specialNeeds: [],
  },
  selectedSeatLocation: {
    tableAlias: "",
    banquetTableId: "",
  },
});

const handleOpenModifyGuestSeatLocation = (
  guest: GuestSeatLocationManagement,
  table: BanquetTableManagement
) => {
  selectedGuestSeatLocationInfo.value = {
    guest,
    selectedSeatLocation: table,
  };

  dialogDisplayController.onShow();
};

const sortedByTableAliasBanquetTables = computed(() => {
  return [
    ...tables.value.sort((a, b) => {
      if (a.tableAlias === "主桌") return -1;
      if (b.tableAlias === "主桌") return 1;

      const [numA, numB] = [
        parseInt(a.tableAlias.slice(-2)),
        parseInt(b.tableAlias.slice(-2)),
      ];

      return numA - numB;
    }),
    { banquetTableId: "", tableAlias: "未安排座位的賓客" },
  ];
});

const handleModifyGuestSeatLocation = ({
  guestId,
  banquetTableId,
}: {
  guestId: string;
  banquetTableId: string;
}) => {
  // call api

  useAsyncData("c", () =>
    $fetch(`/api/admin/seat-location/guests/`, {
      method: "PATCH",
      body: { guestId, seatLocationId: banquetTableId },
      headers: generateJwtAuthorizeHeader(),
    })
  ).then(() => {
    fetchData();
    dialogDisplayController.onClose()
  });
};
</script>
