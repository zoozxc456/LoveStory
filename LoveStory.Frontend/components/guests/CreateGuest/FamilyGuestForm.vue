<template>
  <div
    class="mx-auto flex w-4/5 max-w-[36rem] flex-col rounded-xl bg-white bg-clip-border text-gray-700 shadow-md"
    :class="['absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2']"
  >
    <div class="flex flex-col gap-4 p-6">
      <h4
        class="block font-sans text-2xl antialiased font-semibold leading-snug tracking-normal text-blue-gray-900"
      >
        新增新賓客資料
      </h4>
      <div>
        <h6>全家福名稱</h6>
        <div class="relative h-11 w-full min-w-[200px] mt-3">
          <input
            class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
            placeholder=" "
            v-model.trim="data.familyName"
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

      <div class="flex gap-2 items-center">
        <div class="flex-1">
          <h6>參與人數</h6>
        </div>
        <div class="relative flex-1">
          <input
            class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
            placeholder=" "
            type="number"
            min="2"
            v-model="attendanceNumber"
          />
        </div>
      </div>
      <div class="relative w-full p-2 rounded-md border">
        <label class="text-xs absolute left-2 -top-2">賓客名單</label>
        <div
          class="w-full"
          v-for="(guest, index) in data.attendance"
          :key="index"
        >
          <div class="ps-3 flex items-center gap-3">
            <div
              @click="handleCollsapeClick(index)"
              :class="[
                'transition-transform duration-500',
                { 'rotate-90': collsapeCollection.has(index) },
              ]"
            >
              <font-awesome-icon :icon="['fas', 'angle-right']" size="1x" />
            </div>
            <h6
              class="block font-sans text-base antialiased font-semibold leading-relaxed tracking-normal text-inherit"
            >
              {{ `來賓${index + 1}` }}
            </h6>
          </div>
          <div
            class="px-4 py-2 border rounded-b"
            v-show="collsapeCollection.has(index)"
          >
            <div class="flex gap-2 items-center">
              <div class="flex-1">
                <h6>賓客名稱</h6>
              </div>
              <div class="flex-1 w-full">
                <input
                  class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50"
                  placeholder=" "
                  v-model.trim="guest.guestName"
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
                <GuestsSpecialNeedDropDownList
                  v-model:special-needs="guest.specialNeeds"
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
                    v-model.trim="guest.remark"
                  ></textarea>
                </div>
              </div>
            </div>
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
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import {
  reactive,
  useBanquetTable,
  useDisplayController,
  type FamilyGuestFormDataType,
} from ".nuxt/imports";

const data = defineModel<FamilyGuestFormDataType>({ required: true });
const attendanceNumber = defineModel<number>("attendanceNumber", {
  default: 2,
});

const emits = defineEmits<{
  "on-select": [];
  cancel: [];
}>();

const collsapeCollection = reactive<Set<number>>(new Set());

const handleCollsapeClick = (index: number) => {
  if (collsapeCollection.has(index)) collsapeCollection.delete(index);
  else collsapeCollection.add(index);
};

const { data: tables } = useBanquetTable();
const displayController = useDisplayController();
</script>
