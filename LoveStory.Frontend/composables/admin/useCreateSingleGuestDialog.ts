const createSingleGuestRequest = async (data: AddGuestManagementRequest) => $fetch('/api/admin/guests/single', { method: "POST", body: data, headers: generateJwtAuthorizeHeader() });

export const useCreateSingleGuestDialog = () => {
  const data = reactive<SingleGuestFormDataType>({
    guestName: "",
    guestRelationship: "",
    isAttended: false,
    remark: "",
    specialNeeds: [],
    seatLocation: null
  });

  const handleCreateGuest = async () => {
    if (validFormData(data)) {
      await createSingleGuestRequest(data);
    }
  };

  const validFormData = ({ guestName, guestRelationship }: SingleGuestFormDataType): boolean => {
    if (guestName === "") return false;
    if (guestRelationship === "") return false;

    return true;
  };

  const reset = () => {
    Object.assign(data, {
      guestName: "",
      guestRelationship: "",
      isAttended: false,
      remark: "",
      specialNeeds: [],
      seatLocation: null
    });
  };

  return {
    data, handleCreateGuest, reset
  };
};