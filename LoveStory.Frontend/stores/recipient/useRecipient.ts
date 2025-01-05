import { defineStore } from 'pinia';
import { useRecipientGuestFilterStore } from './useRecipientGuestFilter';

type RecipientGuestStoreState = {
  data: RecipientGuest[],
  isLoading: boolean;
};

export const useRecipientStore = defineStore('useRecipientStore', () => {
  const state = reactive<RecipientGuestStoreState>({ data: [], isLoading: true });

  const fetchRecipientGuests = async () => {
    const { data } = await fetchRecipientGuestsRequest();
    if (data.value) {
      state.data = data.value;
      state.isLoading = false;
    }
  };

  const refreshRecipientGuests = async () => await fetchRecipientGuests();

  const filterRecipientGuests = () => {
    const filter = useRecipientGuestFilterStore();
    return state.data.filter(x => recipientGuestsFilter(x, filter.filterFields));
  };


  const guestArrive = async (guestId: string, guestType: 'single' | 'family') => {
    const res = await useAsyncData<{ isSuccess: boolean; }>('guest-Arrive', () => $fetch('/api/recipient/guest-arrive', { method: "PATCH", body: { guestId, guestType } as { guestId: string; guestType: 'single' | 'family'; }, headers: generateJwtAuthorizeHeader() }));
    return res.data.value?.isSuccess ?? false;
  };



  return { recipientGuests: filterRecipientGuests, fetchRecipientGuests, guestArrive, refreshRecipientGuests };
});

const fetchRecipientGuestsRequest = () => useAsyncData<RecipientGuest[], ErrorResponse>('fetch-all-users', () => $fetch('/api/recipient/guests', { method: "GET", headers: generateJwtAuthorizeHeader() }));

const recipientGuestsFilter = (guest: RecipientGuest, { maleOrFemale, relationship, guestNameSearchText }: { maleOrFemale: "全部" | "主婚人" | "男方" | "女方" | "共同朋友", relationship: string; guestNameSearchText: string; }): boolean => {
  if (maleOrFemale === "全部") {
    if (guestNameSearchText === "") return true;
    return guest.guestName.includes(guestNameSearchText);
  }

  if ((maleOrFemale === "女方" || maleOrFemale === "男方") && relationship === "全部") {
    if (guestNameSearchText === "") return guest.relationship.includes(maleOrFemale);
    return guest.relationship.includes(maleOrFemale) && guest.guestName.includes(guestNameSearchText);
  }

  return guest.relationship.includes(maleOrFemale) && guest.relationship.includes(relationship) && guest.guestName.includes(guestNameSearchText);
};