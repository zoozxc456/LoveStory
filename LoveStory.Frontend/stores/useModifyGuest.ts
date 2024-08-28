import { defineStore } from "pinia";
const validator = useModifySingleGuestFormDataValidator();
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

export const useModifySingleGuestStore = defineStore('modify-single-guest', {
  state: (): ModifySingleGuestFormState => ({
    data: initialModifySingleGuestFormDataGenerator()
  }),
  getters: {},
  actions: {
    convert(guest: GuestManagement) {
      this.$patch({
        data: {
          guestId: guest.guestId,
          guestName: guest.guestName,
          guestRelationship: guest.guestRelationship,
          isAttended: guest.details[0].isAttended,
          remark: guest.details[0].remark,
          specialNeeds: guest.details[0].specialNeeds,
          createAt: guest.createAt,
          creator: guest.creator,
          seatLocation: guest.details[0].seatLocation
        }
      });
    },
    async modify() {
      if (validator.valid(this.data)) {
        await modifySingleGuestRequest(this.data);
      }
    },
    reset() {
      this.$reset();
    }
  },
});

