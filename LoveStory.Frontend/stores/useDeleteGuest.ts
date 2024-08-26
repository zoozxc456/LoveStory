const deleteGuestRequest = async (data: DeleteGuestFormDataType) => $fetch(`/api/admin/guests/${data.guestId}`, { method: 'DELETE', headers: { ...generateJwtAuthorizeHeader() } });

type DeleteGuestFormState = {
  data: DeleteGuestFormDataType;
};

export const useDeleteGuestStore = defineStore('delete-guest-store', {
  state: (): DeleteGuestFormState => ({
    data: {
      guestId: "",
      guestName: ""
    }
  }),
  getters: {},
  actions: {
    async onDelete() {
      await deleteGuestRequest(this.data);
    },
  }
});