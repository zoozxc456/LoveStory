<template>
  <div
    v-if="displayController.state.isShow"
    class="fixed inset-0 z-[999] grid h-screen w-screen place-items-center bg-black bg-opacity-80 opacity-100 backdrop-blur-sm transition-opacity duration-300"
  >
    <GuestsModifyGuestDialogSingleGuestForm
      v-if="guestData.guestType === 'single'"
      v-model:model-value="singleGuestFormData"
      @on-select="handleModifySingleGuest"
      @cancel="displayController.onClose"
    />

    <GuestsModifyGuestDialogFamilyGuestForm
      v-else
      v-model:model-value="familyGuestFormData"
      v-model:attendance-number="attendanceNumber"
      @on-select="handleModifyfamilyGuest"
      @cancel="displayController.onClose"
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

const {
  data: singleGuestFormData,
  handleModifyGuest: handleModifySingleGuest,
  converter: singleGuestConverter,
} = useModifySingleGuest();

const {
  data: familyGuestFormData,
  attendanceNumber,
  handleModifyGuest: handleModifyfamilyGuest,
  converter: familyGuestConverter,
} = useModifyFamilyGuest();

(() => {
  if (props.guestData.guestType === "single")
    singleGuestConverter(props.guestData);
  else familyGuestConverter(props.guestData);
})();

watchEffect(() => {
  console.log(singleGuestFormData);
  console.log(familyGuestFormData);
});
</script>
