<template>
  <div
    v-if="model.state.isShow"
    class="fixed inset-0 z-[999] grid h-screen w-screen place-items-center bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <guests-create-guest-dialog-select-guest-type-form
      v-if="step == 1"
      v-model:model-value="guestType"
      @update:model-value="handleSelectType"
      @cancel="handleCancel"
    />

    <SingleGuestForm
      v-if="step == 2 && guestType == 'single'"
      v-model:model-value="SingleGuestFormData"
      @cancel="handleCancel"
      @on-select="handleSelect"
    />

    <FamilyGuestForm
      v-if="step == 2 && guestType == 'family'"
      v-model:model-value="FamilyGuestFormData"
      v-model:attendance-number="attendanceNumber"
      @cancel="handleCancel"
      @on-select="handleSelect"
    />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import FamilyGuestForm from "../FamilyGuestForm.vue";
import SingleGuestForm from "../SingleGuestForm.vue";

const step = ref<number>(1);
const guestType = ref<GuestType | null>(null);

const emits = defineEmits(["update:guests"]);
const model = defineModel<IDialogDisplayController>("controller", {
  required: true,
});

const handleSelectType = (modelValue: GuestType | null) => {
  step.value = 2;
};

const handleCancel = () => {
  if (step.value === 1) model.value?.onClose();
  else step.value--;
};

const handleSelect = async () => {
  console.log(guestType.value);
  if (guestType.value === "single") {
    await handleCreateGuest();
  } else {
    await handleCreateFamilyGuest();
  }

  emits("update:guests");
  model.value.onClose();
  step.value = 1;
  guestType.value = null;
};

const { data: SingleGuestFormData, handleCreateGuest } =
  useCreateSingleGuestDialog();
const {
  data: FamilyGuestFormData,
  attendanceNumber,
  handleCreateGuest: handleCreateFamilyGuest,
} = useCreateFamilyGuest();
</script>
