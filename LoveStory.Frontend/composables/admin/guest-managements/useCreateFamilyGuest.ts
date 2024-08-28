import type { ICreateGuest } from "../../../types/GuestManagement/guest-form.type";

const createFamilyGuestRequest = async (data: AddFamilyGuestRequest) => $fetch('/api/admin/guests/family', { method: 'POST', body: data, headers: generateJwtAuthorizeHeader() });

const initialFamilyGuestFormDataGenerator = (initialAttendanceNumber: number): FamilyGuestFormDataType => ({
  familyName: "",
  guestRelationship: "",
  isAttended: false,
  attendance: Array.from(Array(initialAttendanceNumber)).map(() => initialFamilyAttendanceDataGenerator()),
  seatLocation: null
});

const initialFamilyAttendanceDataGenerator = (): FamilyAttendanceDataType => ({
  guestName: "",
  remark: "",
  specialNeeds: []
});

const initialAttendanceNumber: number = 2;

type CreateFamilyGuestFormState = {
  data: FamilyGuestFormDataType;
  attendanceNumber: number;
};

export const useCreateFamilyGuest = (validator: IFormDataValidator<FamilyGuestFormDataType>): ICreateGuest<CreateFamilyGuestFormState> => {
  const state = reactive<CreateFamilyGuestFormState>({
    data: initialFamilyGuestFormDataGenerator(initialAttendanceNumber),
    attendanceNumber: initialAttendanceNumber
  });

  const handleCreate = async () => {
    if (validator.valid(state.data)) {
      await createFamilyGuestRequest(state.data);
    }
  };

  const cleanState = () => {
    Object.assign(state.data, initialFamilyGuestFormDataGenerator(initialAttendanceNumber));
    state.attendanceNumber = initialAttendanceNumber;
  };

  const handleCancel = () => {
    cleanState();
  };

  watch(() => state.data.attendance.length, (newVal, oldVal) => {
    if (newVal > oldVal) {
      state.data.attendance.push(initialFamilyAttendanceDataGenerator());
    }

    else state.data.attendance.pop();
  });

  return {
    state, handleCreate, handleCancel, clean: cleanState
  };
};