<template>
  <div
    v-if="displayController.state.isShow"
    data-dialog-backdrop-close="true"
    class="fixed inset-0 z-[999] grid h-dvh w-dvw place-items-center bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <div
      data-dialog="sign-in-dialog"
      class="relative mx-auto flex w-full max-w-[36rem] flex-col rounded-xl bg-white bg-clip-border text-gray-700 shadow-md"
    >
      <div class="flex flex-col gap-4 p-6">
        <h4
          class="block font-sans text-2xl text-center antialiased font-semibold leading-snug tracking-normal text-blue-gray-900"
        >
          重設密碼
        </h4>
        <div>
          <p class="text-center">
            是否要重設<span>{{ props.data.username }}</span
            >的密碼呢？
          </p>
        </div>
      </div>
      <div class="p-6 pt-0 flex gap-2">
        <div class="flex-1">
          <button
            class="block w-full select-none rounded-lg py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-white bg-rose-500 border-solid border-2 border-rose-500 shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 active:opacity-[0.85] disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
            type="button"
            @click="handlers.onClickModifyButton"
          >
            重設密碼
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
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { useUserManagementStore } from "../../../stores/management/users/useUserManagement";

type ModifyPasswordStatusDialogProps = {
  data: Pick<UserManagement, "userId" | "username">;
};

const emits = defineEmits<{ (e: "modified"): void }>();

const displayController = defineModel<IDisplayController>("displayController", {
  required: true,
});

const props = defineProps<ModifyPasswordStatusDialogProps>();

const handlers = {
  onClickModifyButton: () => {
    useUserManagementStore().modifyPasswordStatus(
      { userId: props.data.userId },
      () => {
        emits("modified");
        handlers.onCancel();
      }
    );
  },
  onClickCancelButton: () => {
    handlers.onCancel();
  },
  onCancel: () => {
    displayController.value.onClose();
  },
};
</script>
