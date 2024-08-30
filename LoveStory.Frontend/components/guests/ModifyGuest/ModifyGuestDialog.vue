<template>
  <div
    v-if="displayController.state.isShow"
    class="fixed inset-0 z-[999] grid h-dvh w-dvw place-items-center bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <GuestsModifyGuestSingleGuestForm
      v-if="props.guestType === 'single'"
      v-model:model-value="modifySingleGuestStore.data"
      @on-select="handlers.onModify"
      @cancel="handlers.onCancel"
    />

    <GuestsModifyGuestFamilyGuestForm
      v-else-if="props.guestType === 'family'"
      v-model:model-value="modifyFamilyGuestStore.data"
      v-model:attendance-number="modifyFamilyGuestStore.attendanceNumber"
      @on-select="handlers.onModify"
      @cancel="handlers.onCancel"
    />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import { useModifySingleGuestStore } from "stores/useModifyGuest";
import { useModifyFamilyGuestStore } from "stores/useModifyFamilyGuest";
// import { watch, type GuestType, type IDisplayController } from ".nuxt/imports";

type ModifyGuestDialogProps = { guestType?: GuestType };

const displayController = defineModel<IDisplayController>("displayController", {
  required: true,
});

const props = defineProps<ModifyGuestDialogProps>();
const emits = defineEmits<{ (e: "modified"): void }>();
const [modifySingleGuestStore, modifyFamilyGuestStore] = [
  useModifySingleGuestStore(),
  useModifyFamilyGuestStore(),
];

const handlers = {
  onCancel: () => {
    if (props.guestType === "single") modifySingleGuestStore.$reset();
    else modifyFamilyGuestStore.$reset();

    displayController.value.onClose();
  },

  onModify: async () => {
    if (props.guestType === "single") await modifySingleGuestStore.modify();
    else await modifyFamilyGuestStore.modify();

    emits("modified");
    handlers.onCancel();
  },
};

watch(
  () => modifyFamilyGuestStore.attendanceNumber,
  (newVal, oldVal) => {
    if (oldVal !== 0) {
      if (newVal > oldVal) modifyFamilyGuestStore.appendNewAttendance();
      else modifyFamilyGuestStore.popAttendance();
    }
  }
);
</script>
