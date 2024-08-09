import type { SingleGuestFormDataType } from "types/GuestManagement/guestFormData.type";

export const useCreateSingleGuestDialog = () => {
  const data = reactive<SingleGuestFormDataType>({
    guestName: "",
    guestRelationship: "",
    isAttended: false,
    remark: "",
    specialNeeds: [],
    seatLocation: {
      banquetTableId: ""
    }
  });

  const handleCreateGuest = async () => {
    console.log(data, 'call');
    if (validFormData(data)) {
      console.log(data);
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