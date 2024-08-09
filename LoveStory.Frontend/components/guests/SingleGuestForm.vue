<template>
  <div
    class="relative mx-auto flex w-full max-w-[36rem] flex-col rounded-xl bg-white bg-clip-border text-gray-700 shadow-md"
  >
    <div class="flex flex-col gap-4 p-6">
      <h4
        class="block font-sans text-2xl antialiased font-semibold leading-snug tracking-normal text-blue-gray-900"
      >
        新增新賓客資料
      </h4>
      <div>
        <h6>賓客名稱</h6>
        <div class="relative h-11 w-full min-w-[200px] mt-3">
          <input
            class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 border-t-transparent text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:border-t-transparent focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
            placeholder=" "
            v-model.trim="data.guestName"
          />
          <label
            class="before:content[' '] after:content[' '] pointer-events-none absolute left-0 -top-1.5 flex h-full w-full select-none !overflow-visible truncate text-[11px] font-normal leading-tight text-gray-500 transition-all before:pointer-events-none before:mt-[6.5px] before:mr-1 before:box-border before:block before:h-1.5 before:w-2.5 before:rounded-tl-md before:border-t before:border-l before:border-blue-gray-200 before:transition-all after:pointer-events-none after:mt-[6.5px] after:ml-1 after:box-border after:block after:h-1.5 after:w-2.5 after:flex-grow after:rounded-tr-md after:border-t after:border-r after:border-blue-gray-200 after:transition-all peer-placeholder-shown:text-sm peer-placeholder-shown:leading-[4.1] peer-placeholder-shown:text-blue-gray-500 peer-placeholder-shown:before:border-transparent peer-placeholder-shown:after:border-transparent peer-focus:text-[11px] peer-focus:leading-tight peer-focus:text-gray-900 peer-focus:before:border-t-2 peer-focus:before:border-l-2 peer-focus:before:!border-gray-900 peer-focus:after:border-t-2 peer-focus:after:border-r-2 peer-focus:after:!border-gray-900 peer-disabled:text-transparent peer-disabled:before:border-transparent peer-disabled:after:border-transparent peer-disabled:peer-placeholder-shown:text-blue-gray-500"
          >
            賓客名稱
          </label>
        </div>
      </div>

      <div>
        <h6>賓客關係</h6>
        <GuestsRelationshipDropDownList
          v-model:relationship.trim="data.guestRelationship"
        />
      </div>

      <div class="flex w-full items-center">
        <h6
          class="flex-1 block font-sans text-base antialiased font-semibold leading-relaxed tracking-normal text-inherit"
        >
          是否出席
        </h6>
        <div class="relative flex-1 flex gap-2">
          <div>
            <input type="radio" v-model.trim="data.isAttended" :value="true" />
            <label class="mx-1">是</label>
          </div>
          <div>
            <input type="radio" v-model.trim="data.isAttended" :value="false" />
            <label class="mx-1">否</label>
          </div>
        </div>
      </div>

      <div class="flex w-full items-center" v-if="data.isAttended">
        <h6
          class="flex-1 block font-sans text-base antialiased font-semibold leading-relaxed tracking-normal text-inherit"
        >
          桌位
        </h6>
        <div class="flex-1">
          <GuestsSeatLocationDropDownList
            :tables="tables"
            v-model:model="data.seatLocation"
            v-model:display-controller="displayController"
            @update:model="handleUpdate"
          />
        </div>
      </div>

      <div class="w-full">
        <h6
          class="flex-1 block font-sans text-base antialiased font-semibold leading-relaxed tracking-normal text-inherit"
        >
          特殊需求
        </h6>
        <div class="flex-1">
          <SpecialNeedDropDownList v-model:special-needs="data.specialNeeds" />
        </div>
      </div>

      <div class="w-full">
        <h6
          class="block font-sans text-base antialiased font-semibold leading-relaxed tracking-normal text-inherit"
        >
          備註
        </h6>
        <div class="relative">
          <div>
            <textarea
              class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50 resize-none"
              v-model.trim="data.remark"
            ></textarea>
          </div>
        </div>
      </div>
    </div>
    <div class="p-6 pt-0 flex gap-2">
      <div class="flex-1">
        <button
          class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-white bg-pink-300 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
          type="button"
          @click="emits('on-select')"
        >
          新增
        </button>
      </div>
      <div class="flex-1">
        <button
          class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-gray-400 border-solid border-2 border-gray-300 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
          type="button"
          @click="emits('cancel')"
        >
          取消
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { SingleGuestFormDataType } from "types/GuestManagement/guestFormData.type";
import SpecialNeedDropDownList from "./SpecialNeedDropDownList.vue";
import { useBanquetTable } from "../../composables/admin/useBanquetTable";

const data = defineModel<SingleGuestFormDataType>({ default: {} });

const emits = defineEmits<{
  "on-select": [];
  cancel: [];
}>();

const selectTable = ref<IBanquetTable | null>(null);
const displayController = useDialogDisplayController();

const { data: tables } = useBanquetTable();
watchEffect(() => {
  console.log(tables.value);
});

const handleUpdate = (model: any) => {
  console.log(model);
};
</script>
