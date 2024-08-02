export type CreateGuestFormDataType = {
  guestName: string;
  guestRelationship: string;
  guestType: string;
  isAttended: boolean;
  remark: string;
  specialNeeds: string[];
};

export const useCreateGuestDialog = () => {
  const data = reactive<CreateGuestFormDataType>({
    guestName: "",
    guestRelationship: "",
    guestType: "",
    isAttended: false,
    remark: "",
    specialNeeds: []
  });

  const handleCreateGuest = async (formData: CreateGuestFormDataType) => {
    if (validFormData(formData)) {
      await addGuest(formData);
    }
  };

  const validFormData = ({ guestName, guestRelationship, guestType }: CreateGuestFormDataType): boolean => {
    if (guestName === "") return false;
    if (guestRelationship === "") return false;

    return true;
  };

  return {
    data, handleCreateGuest
  };
};