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

const validator = useModifyFamilyGuestFormDataValidator();

type ModifyFamilyGuestFormState = {
  data: ModifyFamilyGuestFormDataType;
  attendanceNumber: number;
};

export const useModifyFamilyGuestStore = defineStore('modify-family-guest', {
  state: (): ModifyFamilyGuestFormState => ({
    data: initialModifyFamilyGuestFormDataGenerator(),
    attendanceNumber: initialAttendanceNumber
  }),
  getters: {},
  actions: {
    convert(guests: GroupGuestManagement) {
      this.$patch({
        data: {
          guestGroupId: guests.details[0].guestGroup?.guestGroupId,
          guestGroupName: guests.guestName,
          guestRelationship: guests.guestRelationship,
          isAttended: guests.details[0].isAttended,
          attendance: (guests.details as GroupGuestManagementDetail[]).map((detail) => (
            {
              guestName: detail.guestName,
              remark: detail.remark,
              specialNeeds: detail.specialNeeds,
              guestRelationship: guests.guestRelationship,
              guestId: detail.guestId,
              createAt: detail.createAt,
              creator: detail.creator,
              isAttended: detail.isAttended,
              guestGroup: detail.guestGroup,
              seatLocation: detail.seatLocation
            })),
          seatLocation: guests.details[0].seatLocation
        },
        attendanceNumber: guests.details.length
      });
    },
    async modify() {
      if (validator.valid(this.data)) {
        await modifyFamilyGuestsRequest(this.data);
      }
    },
    reset() {
      this.$reset();
    },
    appendNewAttendance() {
      this.data.attendance.push(initialModifyFamilyAttendanceGenerator());
    },
    popAttendance() {
      this.data.attendance.pop();
    }
  }
});