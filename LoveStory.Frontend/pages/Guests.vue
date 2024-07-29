<template>
  <div class="w-full mx-auto">
    <div class="header text-center text-2xl">賓客管理</div>
    <button
      class="select-none rounded-lg bg-gray-900 py-3 px-6 text-center align-middle font-sans text-xs font-bold uppercase text-white shadow-md shadow-gray-900/10 transition-all hover:shadow-lg hover:shadow-gray-900/20 focus:opacity-[0.85] focus:shadow-none active:opacity-[0.85] active:shadow-none disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
      type="button"
      data-dialog-target="sign-in-dialog"
      @click="createGuestDialog.onShow"
    >
      Sign In
    </button>
    <CreateGuestDialog
      v-if="createGuestDialog.isShow"
      @guest-add="handleAddGuest"
    />
    <GuestManagementTable
      :guest-managements="guests"
      @update:guests="refreshGuests"
    />
  </div>
</template>

<style scoped lang="scss"></style>

<script setup lang="ts">
import CreateGuestDialog, {
  type CreateGuestFormDataType,
} from "../components/guests/CreateGuestDialog.vue";
import GuestManagementTable from "../components/guests/GuestManagementTable.vue";
import { useGuestManagement } from "../composables/admin/useGuestManagement";
const { guests, isLoading, addGuestManagement, refreshGuests } =
  useGuestManagement();

const createGuestDialog = reactive<{
  isShow: boolean;
  onShow: () => void;
  onClose: () => void;
}>({
  isShow: false,
  onShow: () => {},
  onClose: () => {},
});

createGuestDialog.onShow = () => (createGuestDialog.isShow = true);
createGuestDialog.onClose = () => (createGuestDialog.isShow = false);

const handleAddGuest = async (formData: CreateGuestFormDataType) => {
  if (validFormData(formData)) {
    await addGuestManagement(formData);
    createGuestDialog.onClose();
    refreshGuests();
  }
};

const validFormData = ({
  guestName,
  guestRelationship,
  guestType,
}: CreateGuestFormDataType): boolean => {
  console.log(guestName, guestRelationship, guestType);
  if (guestName === "") return false;
  if (guestRelationship === "") return false;
  if (guestType === "") return false;

  return true;
};
</script>
