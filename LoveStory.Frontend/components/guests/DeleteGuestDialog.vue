<template>
  <div
    data-dialog-backdrop-close="true"
    class="fixed inset-0 z-[999] grid h-screen w-screen place-items-center bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <div
      data-dialog="sign-in-dialog"
      class="relative mx-auto flex w-full max-w-[36rem] flex-col rounded-xl bg-white bg-clip-border text-gray-700 shadow-md"
    >
      <div class="flex flex-col gap-4 p-6">
        <h4
          class="block font-sans text-2xl text-center antialiased font-semibold leading-snug tracking-normal text-blue-gray-900"
        >
          刪除賓客
        </h4>
        <div>
          <p class="text-center">
            是否要刪除<span>{{ props.guestName }}</span
            >這組賓客呢？
          </p>
        </div>
      </div>
      <div class="p-6 pt-0 flex gap-2">
        <div class="flex-1">
          <button
            class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-white bg-rose-500 border-solid border-2 border-rose-500 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
            type="button"
            @click="emits('delete-guest', props.guestId)"
          >
            刪除
          </button>
        </div>
        <div class="flex-1">
          <button
            class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-gray-400 border-solid border-2 border-gray-300 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
            type="button"
            @click="emits('close-dialog')"
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
const props = defineProps<{
  guestName: string;
  guestId: string;
}>();
const emits = defineEmits(["delete-guest", "close-dialog"]);
</script>
