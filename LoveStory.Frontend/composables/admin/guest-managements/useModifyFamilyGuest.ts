const modifyFamilyGuestsRequest = async (data: ModifyFamilyGuestRequest) => $fetch('/api/admin/guests/family', { method: 'PUT', body: data, headers: generateJwtAuthorizeHeader() });

const initialAttendanceNumber: number = 0;
const initialModifyFamilyGuestFormDataGenerator = (): ModifyFamilyGuestFormDataType => ({
  guestGroupName: "",
  guestRelationship: "",
  isAttended: false,
  attendance: [],
  guestGroupId: "",
  seatLocation: null
});

const initialModifyFamilyAttendanceGenerator = (): ModifySingleGuestFormDataType => ({
  guestName: "",
  remark: "",
  specialNeeds: [],
  guestId: "00000000-0000-0000-0000-000000000000",
  guestRelationship: "",
  guestGroup: null,
  isAttended: false,
  createAt: new Date(),
  creator: {
    userId: "00000000-0000-0000-0000-000000000000",
    username: ""
  },
  seatLocation: null
});

type ModifyFamilyGuestFormState = {
  data: ModifyFamilyGuestFormDataType;
  attendanceNumber: number;
};

export const useModifyFamilyGuest = (validator: IFormDataValidator<ModifyFamilyGuestFormDataType>): IModifyGuest<ModifyFamilyGuestFormState,GroupGuestManagement> => {
  const state = reactive<ModifyFamilyGuestFormState>({
    data: initialModifyFamilyGuestFormDataGenerator(),
    attendanceNumber: initialAttendanceNumber
  });

  const convert = (guestManagementData: GroupGuestManagement): void => {
    state.data.guestGroupId = guestManagementData.details[0].guestGroup?.guestGroupId ?? "";
    state.data.guestGroupName = guestManagementData.guestName;
    state.data.guestRelationship = guestManagementData.guestRelationship;
    state.data.isAttended = guestManagementData.details[0].isAttended;
    state.data.attendance = (guestManagementData.details as GroupGuestManagementDetail[]).map((detail) => (
      {
        guestName: detail.guestName,
        remark: detail.remark,
        specialNeeds: detail.specialNeeds,
        guestRelationship: guestManagementData.guestRelationship,
        guestId: detail.guestId,
        createAt: detail.createAt,
        creator: detail.creator,
        isAttended: detail.isAttended,
        guestGroup: detail.guestGroup,
        seatLocation: detail.seatLocation
      })
    );

    state.data.seatLocation = guestManagementData.details[0].seatLocation;
    state.attendanceNumber = state.data.attendance.length;
  };

  const handleModify = async () => {
    if (validator.valid(state.data)) {
      await modifyFamilyGuestsRequest(state.data);
    }
  };

  const handleCancel = () => cleanForm();

  const cleanForm = () => {
    Object.assign(state.data, initialModifyFamilyGuestFormDataGenerator());

    state.attendanceNumber = initialAttendanceNumber;
  };

  watch(() => state.attendanceNumber, (newVal, oldVal) => {
    if (newVal > oldVal && oldVal !== 0) {
      state.data.attendance.push(initialModifyFamilyAttendanceGenerator());
    }

    else if (newVal < oldVal) state.data.attendance.pop();
  });

  return { state, handleModify, convert, clean: cleanForm, handleCancel };
};