import type { SingleGuestFormDataType } from "types/GuestManagement/guestFormData.type";

export const useCreateSingleGuestDialog = () => {
  const data = reactive<SingleGuestFormDataType>({
    guestName: "",
    guestRelationship: "",
    isAttended: false,
    remark: "",
    specialNeeds: []
  });

  const handleCreateGuest = async () => {
    if (validFormData(data)) {
      await addGuest(data);
    }
  };

  const validFormData = ({ guestName, guestRelationship }: SingleGuestFormDataType): boolean => {
    if (guestName === "") return false;
    if (guestRelationship === "") return false;

    return true;
  };

  return {
    data, handleCreateGuest
  };
};