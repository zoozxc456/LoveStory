import { defineStore } from 'pinia';

type RecipientGuestStoreState = {
  data: RecipientGuest[],
  isLoading: boolean;
};

export const useRecipientStore = defineStore('useRecipientStore', () => {
  const state = reactive<RecipientGuestStoreState>({ data: [], isLoading: true });

  const fetchRecipientGuests = async () => {
    const { data } = await fetchRecipientGuestsRequest();
    console.log(data.value);
    if (data.value) {
      state.data = data.value;
      state.isLoading = false;
    }
  };

  const guestArrive = async (guestId: string) => {
    const res = await useAsyncData<{ isSuccess: boolean; }>('guest-Arrive', () => $fetch('/api/recipient/guest-arrive', { method: "PATCH", body: { guestId } as { guestId: string; }, headers: generateJwtAuthorizeHeader() }));
    return res.data.value?.isSuccess ?? false;
  };

  return { state, fetchRecipientGuests, guestArrive };
});

const fetchRecipientGuestsRequest = () => useAsyncData<RecipientGuest[], ErrorResponse>('fetch-all-users', () => $fetch('/api/recipient/guests', { method: "GET", headers: generateJwtAuthorizeHeader() }));