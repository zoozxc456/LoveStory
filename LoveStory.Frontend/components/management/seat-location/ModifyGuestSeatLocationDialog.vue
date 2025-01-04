<template>
  <div
    v-if="displayController.state.isShow"
    class="fixed inset-0 z-[999] grid h-dvh w-dvw place-items-center bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <div
      class="relative mx-auto flex w-full max-w-[36rem] flex-col rounded-xl bg-white bg-clip-border text-gray-700 shadow-md"
    >
      <div class="flex flex-col gap-4 p-6">
        <h4
          class="block font-sans text-2xl antialiased font-semibold leading-snug tracking-normal text-blue-gray-900"
        >
          修改賓客座位
        </h4>
        <div>
          <h6>賓客名稱</h6>
          <div class="relative h-11 w-full min-w-[200px] mt-3">
            <input
              class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:bg-blue-gray-50"
              placeholder=" "
              :value="selectedSeatLocationInfo.guest?.guestName"
              readonly
              disabled
            />
          </div>
        </div>

        <div>
          <h6>賓客座位</h6>
          <CommonDropDownList
            v-model:display-controller="listDisplayController"
          >
            <template #presentation>
              <input
                class="w-full h-full p-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
                placeholder=" "
                readonly
                @click="listDisplayController.onToggle"
                v-model="
                  selectedSeatLocationInfo.selectedSeatLocation.tableAlias
                "
              />
            </template>
            <template #list>
              <div
                class="absolute right-0 z-10 mt-2 w-56 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none max-h-[250px] overflow-y-auto"
                :class="[
                  listDisplayController.state.isShow ? 'block' : 'hidden',
                ]"
                role="menu"
                aria-orientation="vertical"
                aria-labelledby="menu-button"
                tabindex="-1"
              >
                <div class="py-1" role="none">
                  <div
                    v-for="table in props.tables"
                    :key="table.banquetTableId"
                    class="px-4 py-2 text-sm"
                    @click.stop="handleSelectItem(table)"
                  >
                    {{ table.tableAlias }}
                  </div>
                </div>
              </div>
            </template>
          </CommonDropDownList>
        </div>
      </div>
      <div class="p-6 pt-0 flex gap-2">
        <div class="flex-1">
          <button
            class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-white bg-pink-300 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
            type="button"
            @click="
              emits('on-modify', {
                guestId: selectedSeatLocationInfo.guest.guestId,
                banquetTableId:
                  selectedSeatLocationInfo.selectedSeatLocation.banquetTableId,
              })
            "
          >
            修改
          </button>
        </div>
        <div class="flex-1">
          <button
            class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-gray-400 border-solid border-2 border-gray-300 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
            type="button"
            @click="displayController.onClose"
          >
            取消
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
type ModifyGuestSeatLocationDialogProps = {
  tables: BanquetTableManagement[];
};

const displayController = defineModel<IDisplayController>("displayController", {
  required: true,
});

const listDisplayController = useDisplayController();

const props = defineProps<ModifyGuestSeatLocationDialogProps>();
const emits = defineEmits<{
  (
    e: "on-modify",
    { guestId, banquetTableId }: { guestId: string; banquetTableId: string }
  ): void;
}>();

const selectedSeatLocation = defineModel<{
  guest: GuestSeatLocationManagement;
  selectedSeatLocation: BanquetTableManagement;
}>("selectedSeatLocationInfo", { required: true });

const handleSelectItem = (table: BanquetTableManagement) => {
  selectedSeatLocation.value.selectedSeatLocation = table;
  listDisplayController.onClose();
};
</script>
