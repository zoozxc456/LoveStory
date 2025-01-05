<template>
  <div
    v-if="displayController.state.isShow"
    class="w-[100dvw] h-[100dvh] absolute z-50 bg-black/80 top-0 left-0"
  >
    <div class="relative w-full h-full">
      <div
        class="absolute px-3 top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-[900px] h-[600px] bg-white border rounded-3xl border-gray-700 p-3 grid grid-cols-2 items-center text-xl"
      >
        <div class="h-full border-r border-black">
          <h3 class="text-2xl text-center font-bold">賓客資訊</h3>
          <div class="p-6 grid grid-row-6 h-[90%] space-y-10">
            <div class="grid grid-cols-2 items-center text-xl">
              <label for="">賓客姓名:</label>
              <div class="border-b-2 border-gray-700 px-3">
                {{ props.guestName }}
              </div>
            </div>
            <div class="grid grid-cols-2 items-center text-xl">
              <label for="">人數:</label>
              <div class="border-b-2 border-gray-700 px-3">
                {{ props.attendanceAmount }}
              </div>
            </div>
            <div class="grid grid-cols-2 items-center text-xl">
              <label for="">桌位:</label>
              <div class="border-b-2 border-gray-700 px-3">
                {{ props.seatLocation ?? "尚未安排" }}
              </div>
            </div>
            <div class="grid grid-cols-2 items-center text-xl">
              <label for="">特殊需求:</label>
              <div class="border-b-2 border-gray-700 px-3">
                {{ props.specialNeed?.join(",") ?? "無" }}
              </div>
            </div>
            <div class="grid grid-cols-2 items-center text-xl">
              <label for="">備註:</label>
              <div class="border-b-2 border-gray-700 px-3">
                {{ props.remark || "無" }}
              </div>
            </div>
            <div class="text-center">
              <button
                class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-lg font-bold uppercase text-white bg-pink-300 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
                type="button"
                @click="displayController.onClose"
              >
                完成
              </button>
            </div>
          </div>
        </div>

        <div class="h-full px-3">
          <div class="max-w-[600px] max-h-[550px] h-[100%]">
            <img
              src="/assets/images/table-map.png"
              alt="table-map"
              class="w-full h-full"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import type { RecipientGuestOverview } from "../../types/recipients/recipientGuest.type";

const props = defineProps<RecipientGuestOverview>();
const displayController = defineModel<IDisplayController>("displayController", {
  required: true,
});
</script>
