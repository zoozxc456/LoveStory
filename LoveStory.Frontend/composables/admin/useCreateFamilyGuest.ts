import { addFamilyGuest } from "../../apis/guest.api";
import type { CreateFamilyGuestFormDataType, FamilyAttendanceDataType } from "./dialog.type";

export const useCreateFamilyGuest = () => {
  const attendanceNumber = ref<number>(2);

  const data = reactive<CreateFamilyGuestFormDataType>({
    familyName: "",
    relationship: "",
    isAttended: false,
    attendance: [...Array(attendanceNumber.value)].map<FamilyAttendanceDataType>(() => ({
      guestName: "",
      remark: "",
      specialNeeds: [],
    }))
  });

  const handleCreateGuest = async (formData: CreateFamilyGuestFormDataType) => {
    if (validFormData(formData)) {
      await addFamilyGuest(formData);
    }
  };

  const validFormData = ({ }: CreateFamilyGuestFormDataType): boolean => {
    // if (guestName === "") return false;
    // if (guestRelationship === "") return false;
    // if (guestType === "") return false;

    return true;
  };

  watch(attendanceNumber, (newVal, oldVal) => {
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