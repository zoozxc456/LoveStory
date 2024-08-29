<template>
  <div
    v-if="displayController.state.isShow"
    class="fixed inset-0 z-[999] h-dvh w-dvw bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <div class="relative w-full h-full">
      <GuestsCreateGuestSelectGuestTypeForm
        v-if="dialogStates.currentProccessStep == 1"
        v-model:model-value="dialogStates.currentGuestType"
        @update:model-value="handlers.onSelectGuestType"
        @cancel="handlers.onCancel"
      />

      <GuestsCreateGuestSingleGuestForm
        v-if="
          dialogStates.currentProccessStep == 2 &&
          dialogStates.currentGuestType == 'single'
        "
        v-model:model-value="createSingleGuestProvider.state.data"
        @cancel="handlers.onBack"
        @on-select="handlers.onCreate"
      />

      <GuestsCreateGuestFamilyGuestForm
        v-if="
          dialogStates.currentProccessStep == 2 &&
          dialogStates.currentGuestType == 'family'
        "
        v-model:model-value="createFamilyGuestProvider.state.data"
        v-model:attendance-number="
          createFamilyGuestProvider.state.attendanceNumber
        "
        @cancel="handlers.onBack"
        @on-select="handlers.onCreate"
      />
    </div>
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import {
  reactive,
  useCreateSingleGuest,
  useCreateFamilyGuest,
  useSingleGuestFormDataValidator,
  useFamilyGuestsFormDataValidator,
  type GuestType,
  type IDisplayController,
} from ".nuxt/imports";

const dialogStates = reactive<{
  currentProccessStep: number;
  currentGuestType: GuestType | null;
}>({ currentProccessStep: 1, currentGuestType: null });

const emits = defineEmits<{ (e: "created"): void }>();
const displayController = defineModel<IDisplayController>("displayController", {
  required: true,
});

const [createSingleGuestProvider, createFamilyGuestProvider] = [
  useCreateSingleGuest(useSingleGuestFormDataValidator()),
  useCreateFamilyGuest(useFamilyGuestsFormDataValidator()),
];

const handlers = {
  onSelectGuestType: () => {
    dialogStates.currentProccessStep++;
  },
  onCancel: () => {
    displayController.value.onClose();
  },
  onBack: () => {
    createSingleGuestProvider.clean();
    createFamilyGuestProvider.clean();

    dialogStates.currentProccessStep--;
  },
  onCreate: async () => {
    if (dialogStates.currentGuestType === "single")
      await createSingleGuestProvider.handleCreate();
    else await createFamilyGuestProvider.handleCreate();

    createSingleGuestProvider.clean();
    createFamilyGuestProvider.clean();

    emits("created");
    displayController.value.onClose();

    dialogStates.currentGuestType = null;
    dialogStates.currentProccessStep = 1;
  },
};
</script>
