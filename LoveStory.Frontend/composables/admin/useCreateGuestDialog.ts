import { addGuest } from "../../apis/guest.api";
import type { IDialogDisplayController } from "./useDialogDisplayController";

type CreateGuestFormDataType = {
  guestName: string;
  guestRelationship: string;
  guestType: string;
  isAttended: boolean;
  remark: string;
};

export const useCreateGuestDialog = (displayController: IDialogDisplayController, emits: (event: 'update:guests') => void) => {
  const data = reactive<CreateGuestFormDataType>({
    guestName: "",
    guestRelationship: "",
    guestType: "",
    isAttended: false,
    remark: ""
  });

  const handleTriggerShowDialog = () => {
    displayController.onShow();
  };

  const handleCancel = () => {
    displayController.onClose();
  };

  const handleCreateGuest = async (formData: CreateGuestFormDataType) => {
    if (validFormData(formData)) {
      await addGuest(formData);
      displayController.onClose();
      emits('update:guests');
    }
  };

  const validFormData = ({ guestName, guestRelationship, guestType }: CreateGuestFormDataType): boolean => {
    if (guestName === "") return false;
    if (guestRelationship === "") return false;
    if (guestType === "") return false;

    return true;
  };

  return {
    data, isShow: computed(() => displayController.state.isShow),
    handleCancel, handleCreateGuest, handleTriggerShowDialog
  };
};