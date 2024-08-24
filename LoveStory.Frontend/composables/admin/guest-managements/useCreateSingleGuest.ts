const createSingleGuestRequest = async (data: AddGuestManagementRequest) => $fetch('/api/admin/guests/single', { method: "POST", body: data, headers: generateJwtAuthorizeHeader() });

const inintialSingleGuestFormDataGenerator = (): SingleGuestFormDataType => ({
  guestName: "",
  guestRelationship: "",
  isAttended: false,
  remark: "",
  specialNeeds: [],
  seatLocation: null
});

type CreateSingleGuestFormState = {
  data: SingleGuestFormDataType;
};

export const useCreateSingleGuest = (validator: IFormDataValidator<SingleGuestFormDataType>): ICreateGuest<CreateSingleGuestFormState> => {
  const state = reactive<CreateSingleGuestFormState>({ data: inintialSingleGuestFormDataGenerator() });

  const handleCreate = async () => {
    if (validator.valid(state.data)) {
      await createSingleGuestRequest(state.data);
    }
  };

  const handleCancel = () => {
    cleanState();
  };

  const cleanState = () => {
    Object.assign(state.data, inintialSingleGuestFormDataGenerator());
  };

  return {
    state, handleCreate, handleCancel, clean: cleanState
  };
};