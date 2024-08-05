import type { FamilyGuestFormDataType } from "types/GuestManagement/guestFormData.type";

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
    }))
  });

  const handleCreateGuest = async () => {
    if (validFormData(data)) {
      await addFamilyGuest(data);
    }
  };

  const validFormData = ({ }: FamilyGuestFormDataType): boolean => {
    // if (guestName === "") return false;
    // if (guestRelationship === "") return false;
    // if (guestType === "") return false;

    return true;
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
    data, attendanceNumber, handleCreateGuest
  };
};