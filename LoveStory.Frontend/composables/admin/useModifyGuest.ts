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
    },
    seatLocation: {
      banquetTableId: ""
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
    data.seatLocation = guestManagementData.details[0].seatLocation;
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

  const data = reactive<ModifyFamilyGuestFormDataType>({
    guestGroupName: "",
    guestRelationship: "",
    isAttended: false,
    attendance: [],
    guestGroupId: "",
    seatLocation: {
      banquetTableId: ""
    }
  });

  const converter = (guestManagementData: GroupGuestManagement): void => {
    console.log(guestManagementData);
    data.guestGroupId = guestManagementData.details[0].guestGroup?.guestGroupId ?? "";
    data.guestGroupName = guestManagementData.guestName;
    data.guestRelationship = guestManagementData.guestRelationship;
    data.isAttended = guestManagementData.details[0].isAttended;
    data.attendance = (guestManagementData.details as GroupGuestManagementDetail[]).map((detail) => (
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

    data.seatLocation = guestManagementData.details[0].seatLocation;
    attendanceNumber.value = data.attendance.length;
  };

  const handleModifyGuest = async () => {
    console.log(`===== Start Console.log for handle family modify =====`);
    console.log(data);
    console.log(`===== End Console.log for handle family modify =====`);
    modifyFamilyGuest(data);
  };


  watch(() => attendanceNumber.value, (newVal, oldVal) => {
    if (newVal > oldVal && oldVal !== 0) {
      data.attendance.push({
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
        seatLocation: {
          banquetTableId: ""
        }
      });
    }

    else if (newVal < oldVal) data.attendance.pop();
  });

  return { data, attendanceNumber, handleModifyGuest, converter };
};