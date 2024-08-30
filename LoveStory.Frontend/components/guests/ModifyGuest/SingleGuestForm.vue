<template>
  <div
    class="relative mx-auto flex w-full max-w-[36rem] flex-col rounded-xl bg-white bg-clip-border text-gray-700 shadow-md"
  >
    <div class="flex flex-col gap-4 p-6">
      <h4
        class="block font-sans text-2xl antialiased font-semibold leading-snug tracking-normal text-blue-gray-900"
      >
        修改賓客資料
      </h4>
      <div>
        <h6>賓客名稱</h6>
        <div class="relative h-11 w-full min-w-[200px] mt-3">
          <input
            class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
            placeholder=" "
            v-model.trim="data.guestName"
          />
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
          <GuestsModifyGuestSpecialNeedDropDownList
            v-model:special-needs="data.specialNeeds"
          />
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
          修改
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
// import {
//   useDisplayController,
//   useBanquetTable,
//   type ModifySingleGuestFormDataType,
// } from ".nuxt/imports";

const data = defineModel<ModifySingleGuestFormDataType>({ default: {} });

const emits = defineEmits<{
  "on-select": [];
  cancel: [];
}>();

const displayController = useDisplayController();
const { data: tables } = useBanquetTable();
</script>
