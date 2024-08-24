<template>
  <div
    v-if="model.state.isShow"
    class="fixed inset-0 z-[999] grid h-screen w-screen place-items-center bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <SelectGuestTypeForm
      v-if="step == 1"
      v-model:model-value="guestType"
      @update:model-value="handleSelectType"
      @cancel="handleCancel"
    />

    <SingleGuestForm
      v-if="step == 2 && guestType == 'single'"
      v-model:model-value="singleGuestFormDataState.data"
      @cancel="handleCancel"
      @on-select="handleSelect"
    />

    <FamilyGuestForm
      v-if="step == 2 && guestType == 'family'"
      v-model:model-value="familyGuestFormDataState.data"
      v-model:attendance-number="familyGuestFormDataState.attendanceNumber"
      @cancel="handleCancel"
      @on-select="handleSelect"
    />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import SelectGuestTypeForm from "./SelectGuestTypeForm.vue";
import FamilyGuestForm from "./FamilyGuestForm.vue";
import SingleGuestForm from "./SingleGuestForm.vue";

const step = ref<number>(1);
const guestType = ref<GuestType | null>(null);

const emits = defineEmits(["update:guests"]);
const model = defineModel<IDisplayController>("controller", {
  required: true,
});

const handleSelectType = (modelValue: GuestType | null) => {
  step.value = 2;
};

const handleCancel = () => {
  if (step.value === 1) model.value?.onClose();
  else {
    handleCancelSingleGuestForm();
    handleCancelFamilyGuestForm();
    step.value--;
  }
};

const handleSelect = async () => {
  if (guestType.value === "single") {
    await handleCreateSingleGuestForm();
    cleanSingleGuestForm();
  } else {
    await handleCreateFamilyGuestForm();
    cleanFamilyGuestForm();
  }

  emits("update:guests");
  model.value.onClose();
  step.value = 1;
  guestType.value = null;
};

const {
  state: singleGuestFormDataState,
  handleCreate: handleCreateSingleGuestForm,
  handleCancel: handleCancelSingleGuestForm,
  clean: cleanSingleGuestForm,
} = useCreateSingleGuest(useSingleGuestFormDataValidator());

const {
  state: familyGuestFormDataState,
  handleCreate: handleCreateFamilyGuestForm,
  handleCancel: handleCancelFamilyGuestForm,
  clean: cleanFamilyGuestForm,
} = useCreateFamilyGuest(useFamilyGuestsFormDataValidator());
</script>
