const modifySingleGuestRequest = async (data: ModifySingleGuestRequest) => $fetch('/api/admin/guests/single', { method: 'PUT', body: data, headers: generateJwtAuthorizeHeader() });

const initialModifySingleGuestFormDataGenerator = (): ModifySingleGuestFormDataType => ({
  guestName: "",
  guestRelationship: "",
  isAttended: false,
  remark: "",
  specialNeeds: [],
  guestId: "",
  guestGroup: null,
  createAt: new Date(),
  creator: {
    userId: "",
    username: ""
  },
  seatLocation: null
});

type ModifySingleGuestFormState = {
  data: ModifySingleGuestFormDataType;
};

export const useModifySingleGuest = (validator: IFormDataValidator<ModifySingleGuestFormDataType>): IModifyGuest<ModifySingleGuestFormState,GuestManagement> => {
  const state = reactive<ModifySingleGuestFormState>({
    data: initialModifySingleGuestFormDataGenerator()
  });

  const convert = (guestManagementData: GuestManagement): void => {
    state.data.guestId = guestManagementData.guestId;
    state.data.guestName = guestManagementData.guestName;
    state.data.guestRelationship = guestManagementData.guestRelationship;
    state.data.isAttended = guestManagementData.details[0].isAttended;
    state.data.remark = guestManagementData.details[0].remark;
    state.data.specialNeeds = guestManagementData.details[0].specialNeeds;
    state.data.createAt = guestManagementData.createAt;
    state.data.creator = guestManagementData.creator;
    state.data.seatLocation = guestManagementData.details[0].seatLocation;
  };

  const handleModify = async () => {
    if (validator.valid(state.data)) {
      await modifySingleGuestRequest(state.data);
    }
  };

  const handleCancel = () => cleanData();

  const cleanData = () => {
    Object.assign(state.data, initialModifySingleGuestFormDataGenerator());
  };

  return { state, handleModify, handleCancel, convert, clean: cleanData };
};