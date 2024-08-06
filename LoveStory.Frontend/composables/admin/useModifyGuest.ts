import type { ModifySingleGuestFormDataType } from "types/GuestManagement/guestFormData.type";

export const useModifySingleGuest = () => {
  const data = reactive<ModifySingleGuestFormDataType>({
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
    }
  });

  const converter = (guestManagementData: GuestManagement): void => {
    data.guestId = guestManagementData.guestId;
    data.guestName = guestManagementData.guestName;
    data.guestRelationship = guestManagementData.guestRelationship;
    data.isAttended = guestManagementData.details[0].isAttended;
    data.remark = guestManagementData.details[0].remark;
    data.specialNeeds = guestManagementData.details[0].specialNeeds;
    data.createAt = guestManagementData.createAt;
    data.creator = guestManagementData.creator;
  };

  const handleModifyGuest = async () => {
    console.log(`===== Start Console.log for handle single modify =====`);
    console.log(data);
    modifySingleGuest(data);
    console.log(`===== End Console.log for handle single modify =====`);
  };

  return { data, handleModifyGuest, converter };
};

export const useModifyFamilyGuest = () => {
  const attendanceNumber = ref<number>(0);

  const data = reactive<FamilyGuestFormDataType>({
    familyName: "",
    guestRelationship: "",
    isAttended: false,
    attendance: []
  });

  const converter = (guestManagementData: GuestManagement): void => {
    data.familyName = guestManagementData.guestName;
    data.guestRelationship = guestManagementData.guestRelationship;
    data.isAttended = guestManagementData.details[0].isAttended;
    data.attendance = guestManagementData.details.map<FamilyAttendanceDataType>(detail => ({
      guestName: detail.guestName,
      remark: detail.remark,
      specialNeeds: detail.specialNeeds.map(need => need.specialNeedContent)
    }));

    attendanceNumber.value = data.attendance.length;
  };

  const handleModifyGuest = async () => {
    console.log(`===== Start Console.log for handle family modify =====`);
    console.log(data);
    console.log(`===== End Console.log for handle family modify =====`);
    // modifySingleGuest(data);
  };


  watch(() => attendanceNumber.value, (newVal, oldVal) => {
    if (newVal > oldVal && oldVal !== 0) {
      data.attendance.push({
        guestName: "",
        remark: "",
        specialNeeds: []
      });
    }

    else if (newVal < oldVal) data.attendance.pop();
  });

  return { data, attendanceNumber, handleModifyGuest, converter };
};