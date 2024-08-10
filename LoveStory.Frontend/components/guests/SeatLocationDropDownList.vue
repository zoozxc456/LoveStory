<template>
  <CommonDropDownList v-model:display-controller="displayController">
    <template #presentation>
      <input
        class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:border-t-transparent focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50 cursor-pointer"
        placeholder=" "
        readonly
        @click.prevent="displayController.onToggle"
        :value="currentSelectTableAlias"
      />
    </template>
    <template #list>
      <div
        class="absolute right-0 z-10 mt-2 w-full origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
        :class="[displayController.state.isShow ? 'block' : 'hidden']"
        role="menu"
        aria-orientation="vertical"
        aria-labelledby="menu-button"
        tabindex="-1"
      >
        <div class="py-1" role="none">
          <div
            v-for="item in props.tables"
            class="px-4 py-2 text-sm relative"
            @click.prevent="handleSelectItem(item.banquetTableId)"
          >
            <div>{{ item.tableAlias }}</div>
            <div
              v-if="model?.banquetTableId === item.banquetTableId"
              class="w-[16px] h-[16px] absolute top-1/2 right-2 -translate-y-1/2"
            >
              <font-awesome-icon :icon="['fas', 'check']" :size="'2xs'" />
            </div>
          </div>
        </div>
      </div>
    </template>
  </CommonDropDownList>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
type SeatLocationDropDownListProps = {
  tables: IBanquetTable[];
};

const displayController = defineModel<IDialogDisplayController>(
  "displayController",
  { required: true }
);

const model = defineModel<Pick<IBanquetTable, "banquetTableId"> | null>(
  "model",
  {
    required: true,
  }
);

const props = defineProps<SeatLocationDropDownListProps>();

const handleSelectItem = (id: string) => {
  model.value = props.tables.find((x) => x.banquetTableId === id) || null;
  displayController.value.onClose();
};

const currentSelectTableAlias = computed(
  () =>
    props.tables.find((x) => x.banquetTableId === model.value?.banquetTableId)
      ?.tableAlias
);
</script>
