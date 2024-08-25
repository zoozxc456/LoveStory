<template>
  <div
    v-if="displayController.state.isShow"
    class="fixed inset-0 z-[999] grid h-screen w-screen place-items-center bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <GuestsModifyGuestSingleGuestForm
      v-if="guestData?.guestType === 'single'"
      v-model:model-value="singleGuestFormState.data"
      @on-select="handleSelect"
      @cancel="handleCancel"
    />

    <GuestsModifyGuestFamilyGuestForm
      v-else-if="guestData?.guestType === 'group'"
      v-model:model-value="familyGuestFormState.data"
      v-model:attendance-number="familyGuestFormState.attendanceNumber"
      @on-select="handleSelect"
      @cancel="handleCancel"
    />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
type ModifyGuestDialogProps = {
  guestData: GuestManagement;
};

const displayController = defineModel<IDisplayController>(
  "displayController",
  { required: true }
);

const props = defineProps<ModifyGuestDialogProps>();
const emits = defineEmits<{ "update:guests": [] }>();

const {
  state: singleGuestFormState,
  handleModify: handleModifySingleGuest,
  convert: singleGuestConverter,
  clean: cleanSingleGuestForm,
} = useModifySingleGuest(useModifySingleGuestFormDataValidator());

const {
  state: familyGuestFormState,
  handleModify: handleModifyFamilyGuest,
  convert: familyGuestConverter,
  clean: cleanFamilyGuestForm,
} = useModifyFamilyGuest(useModifyFamilyGuestFormDataValidator());

const handleSelect = async () => {
  if (props.guestData.guestType === "single") await handleModifySingleGuest();
  else await handleModifyFamilyGuest();

  emits("update:guests");
  handleCancel();
};

const handleCancel = () => {
  if (props.guestData.guestType === "single") cleanSingleGuestForm();
  else cleanFamilyGuestForm();

  displayController.value.onClose();
};

watch(
  () => displayController.value.state.isShow,
  () => {
    if (props.guestData.guestType === "single")
      singleGuestConverter(props.guestData);
    else familyGuestConverter(props.guestData);
  }
);
</script>
