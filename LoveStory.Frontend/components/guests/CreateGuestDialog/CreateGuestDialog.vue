<template>
  <div
    v-if="model?.state.isShow"
    class="fixed inset-0 z-[999] grid h-screen w-screen place-items-center bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <guests-create-guest-dialog-select-guest-type-form
      v-if="step == 1"
      v-model:model-value="guestType"
      @update:model-value="handleSelectType"
      @cancel="handleCancel"
    />

    <guests-create-guest-dialog-single-guest-form
      v-if="step == 2 && guestType == 'single'"
      v-model:model-value="data"
      @cancel="handleCancel"
      @on-select="handleSelect"
    />

    <guests-create-guest-dialog-family-guest-form
      v-if="step == 2 && guestType == 'family'"
      v-model:model-value="FamilyData"
      v-model:attendance-number="attendanceNumber"
      @cancel="handleCancel"
      @on-select="handleSelect"
    />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
const step = ref<number>(1);
const guestType = ref<GuestType | null>(null);

const emits = defineEmits(["update:guests"]);
const model = defineModel<IDialogDisplayController>("controller");

const handleSelectType = (modelValue: GuestType | null) => {
  console.log(modelValue);
  console.log(guestType.value);
  step.value = 2;
};

const handleCancel = () => {
  if (step.value === 1) model.value?.onClose();
  else step.value--;
};

const handleSelect = async () => {
  if (guestType.value === "single") {
    console.log(data);
    await handleCreateGuest(data);
  } else {
    await handleCreateFamilyGuest(FamilyData);
  }

  emits("update:guests");
  model.value?.onClose();
  step.value = 1;
  guestType.value = null;
};

const { data, handleCreateGuest } = useCreateGuestDialog();
const {
  data: FamilyData,
  attendanceNumber,
  handleCreateGuest: handleCreateFamilyGuest,
} = useCreateFamilyGuest();
</script>
