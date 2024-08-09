<template>
  <div
    v-if="displayController.state.isShow"
    class="fixed inset-0 z-[999] grid h-screen w-screen place-items-center bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <GuestsModifyGuestDialogSingleGuestForm
      v-if="guestData?.guestType === 'single'"
      v-model:model-value="singleGuestFormData"
      @on-select="handleSelect"
      @cancel="handleCancel"
    />

    <GuestsModifyGuestDialogFamilyGuestForm
      v-else-if="guestData?.guestType === 'group'"
      v-model:model-value="familyGuestFormData"
      v-model:attendance-number="attendanceNumber"
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

const displayController = defineModel<IDialogDisplayController>(
  "displayController",
  { required: true }
);

const props = defineProps<ModifyGuestDialogProps>();
const emits = defineEmits<{ "update:guests": [] }>();

const {
  data: singleGuestFormData,
  handleModifyGuest: handleModifySingleGuest,
  converter: singleGuestConverter,
  reset: resetSingleGuestFormData,
} = useModifySingleGuest();

const {
  data: familyGuestFormData,
  attendanceNumber,
  handleModifyGuest: handleModifyFamilyGuest,
  converter: familyGuestConverter,
  reset: resetFamilyGuestFormData,
} = useModifyFamilyGuest();

const handleSelect = async () => {
  if (props.guestData.guestType === "single") await handleModifySingleGuest();
  else await handleModifyFamilyGuest();

  emits("update:guests");
  handleCancel();
};

const handleCancel = () => {
  if (props.guestData.guestType === "single") resetSingleGuestFormData();
  else resetFamilyGuestFormData();

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

watchEffect(() => {
  if (props.guestData !== null) {
  }
});
</script>
