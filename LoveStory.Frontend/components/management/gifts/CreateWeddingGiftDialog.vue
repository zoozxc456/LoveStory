<template>
  <div
    v-if="displayController.state.isShow"
    class="fixed inset-0 z-[999] h-dvh w-dvw bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <div class="relative w-full h-full">
      <div
        class="mx-auto flex w-4/5 max-w-[36rem] flex-col rounded-xl bg-white bg-clip-border text-gray-700 shadow-md"
        :class="['absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2']"
      >
        <div class="flex flex-col gap-4 p-6">
          <h4
            class="block font-sans text-2xl antialiased font-semibold leading-snug tracking-normal text-blue-gray-900"
          >
            新增禮金
          </h4>
          <div class="flex items-center">
            <div class="flex-1">
              <h6>賓客名字</h6>
            </div>
            <div class="flex-[3] relative h-11 w-full min-w-[200px] mt-3">
              <input
                class="w-full h-full p-3 font-sans text-sm text-gray-500 font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:bg-gray-50"
                placeholder=" "
                disabled
                v-model.trim="formData.managementName"
              />
            </div>
          </div>
          <div class="flex items-center">
            <div class="flex-1">
              <h6>賓客關係</h6>
            </div>
            <div class="flex-[3] relative h-11 w-full min-w-[200px] mt-3">
              <input
                class="w-full h-full p-3 font-sans text-sm text-gray-500 font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:bg-gray-50"
                placeholder=" "
                disabled
                v-model.trim="formData.guestRelationship"
              />
            </div>
          </div>
          <div class="flex items-center">
            <div class="flex-1 h-11 mt-3">
              <div class="flex item-center h-full">
                <p class="my-auto">金額</p>
              </div>
            </div>
            <div class="flex-[3] relative h-11 w-full min-w-[200px] mt-3">
              <input
                class="w-full h-full p-3 font-sans text-sm text-gray-500 font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:bg-gray-50"
                placeholder=" "
                type="number"
                min="1"
                v-model.number="formData.amount"
              />
            </div>
          </div>

          <div>
            <div class="flex item-center h-full">
              <p class="my-auto">備註</p>
            </div>

            <div class="relative">
              <textarea
                class="w-full h-full px-3 py-3 font-sans text-sm font-normal transition-all bg-transparent border rounded-md peer border-blue-gray-200 text-blue-gray-700 outline outline-0 placeholder-shown:border placeholder-shown:border-blue-gray-200 placeholder-shown:border-t-blue-gray-200 focus:border-2 focus:border-gray-900 focus:outline-0 disabled:border-0 disabled:bg-blue-gray-50 resize-none"
                v-model.trim="formData.remark"
              ></textarea>
            </div>
          </div>
        </div>
        <div class="p-6 pt-0 flex gap-2">
          <div class="flex-1">
            <button
              class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-white bg-pink-300 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
              type="button"
              @click="handlers.onClickCreateButton"
            >
              新增
            </button>
          </div>
          <div class="flex-1">
            <button
              class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-gray-400 border-solid border-2 border-gray-300 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
              type="button"
              @click="handlers.onClickCancelButton"
            >
              取消
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import {
  useWeddingGiftManagementStore,
  type CreateWeddingGiftFormData,
} from "stores/management/wedding-gifts/useWeddingGiftManagement";
import type { IDisplayController } from ".nuxt/imports";

const emits = defineEmits<{ (e: "created"): void }>();
const displayController = defineModel<IDisplayController>("displayController", {
  required: true,
});

const formData = defineModel<CreateWeddingGiftFormData>("formData", {
  required: true,
});

const handlers = {
  onClickCreateButton: () => {
    useWeddingGiftManagementStore().createWeddingGift(formData.value, () => {
      emits("created");
      handlers.onCancel();
    });
  },
  onClickCancelButton: () => {
    handlers.onCancel();
  },
  onCancel: () => {
    formData.value.managementId = "";
    formData.value.amount = 0;
    formData.value.giftType = "紅包";
    formData.value.managementName = "";
    formData.value.managementType = "single";
    formData.value.guestRelationship = "";
    formData.value.remark = undefined;

    displayController.value.onClose();
  },
};
</script>
