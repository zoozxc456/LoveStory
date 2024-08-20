const createFamilyGuestRequest = async (data: AddFamilyGuestRequest) => $fetch('/api/admin/guests/family', { method: 'POST', body: data, headers: generateJwtAuthorizeHeader() });

export const useCreateFamilyGuest = () => {
  const initialAttendanceNumber = 2;
  const attendanceNumber = ref<number>(initialAttendanceNumber);

  const data = reactive<FamilyGuestFormDataType>({
    familyName: "",
    guestRelationship: "",
    isAttended: false,
    attendance: Array.from(Array(initialAttendanceNumber)).map(() => ({
      guestName: "",
      remark: "",
      specialNeeds: [],
    })),
    seatLocation: null
  });

  const handleCreateGuest = async () => {
    if (validFormData(data)) {
      await createFamilyGuestRequest(data);
    }
  };

  const validFormData = ({ }: FamilyGuestFormDataType): boolean => {
    return true;
  };

  const reset = () => {
    Object.assign(data, {
      familyName: "",
      guestRelationship: "",
      isAttended: false,
      attendance: Array.from(Array(initialAttendanceNumber)).map(() => ({
        guestName: "",
        remark: "",
        specialNeeds: [],
      })),
      seatLocation: null
    });

    attendanceNumber.value = initialAttendanceNumber;
  };

  watch(() => data.attendance.length, (newVal, oldVal) => {
    if (newVal > oldVal) {
      data.attendance.push({
        guestName: "",
        remark: "",
        specialNeeds: []
      });
    }

    else data.attendance.pop();
  });

  return {
    data, attendanceNumber, handleCreateGuest, reset
  };
};