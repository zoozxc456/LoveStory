import type { IGuest } from "types/apis/guest.type";
import { fetchAllGuests } from "../../apis/guest.api";
import { ref, computed } from 'vue';
import type { Ref } from 'vue';
import type { ErrorResponse } from "types/apis/index.type";
import type { GroupGuestManagement, GroupGuestManagementDetail, GuestManagement, SingleGuestManagement, SingleGuestManagementDetail } from "types/GuestManagement/guestManagement.type";
import dayjs from "dayjs";

const dateTimeAscendingComparer = (x: Date, y: Date): number => dayjs(x).unix() - dayjs(y).unix();

const processSingleDetail = (guest: IGuest): SingleGuestManagementDetail => guest;
const processSingleGuest = (data: IGuest[]): SingleGuestManagement[] => data.map((guest) => ({
  guestId: guest.guestId,
  guestName: guest.guestName,
  guestRelationship: guest.guestRelationship,
  createAt: guest.createAt,
  creator: guest.creator,
  guestType: 'single',
  details: [processSingleDetail(guest)]
}));

const processGroupDetail = (guests: IGuest[]): GroupGuestManagementDetail[] => guests.map<GroupGuestManagementDetail>((x) => x).toSorted((x, y) => dateTimeAscendingComparer(x.createAt, y.createAt));

const processGroupGuests = (data: IGuest[]): GroupGuestManagement[] => [...(new Set(data.map(x => x.guestGroup?.guestGroupId).filter(x => x !== undefined)))]
  .map(key => data.filter(guest => guest.guestGroup?.guestGroupId === key))
  .map(guests => {
    const basic = guests[0];
    return ({
      guestId: basic.guestId,
      guestRelationship: basic.guestRelationship,
      guestName: basic.guestGroup?.guestGroupName || guests.map(x => x.guestName).join(','),
      createAt: basic.guestGroup?.createAt,
      creator: basic.creator,
      guestType: 'group',
      details: [...processGroupDetail(guests)]
    } as GroupGuestManagement);
  });

const convertToGuestManagementList = (data: IGuest[]): GuestManagement[] => {
  const singleGuests = data.filter(x => x.guestGroup === null);
  const groupGuests = data.filter(x => x.guestGroup !== null);
  return [...processSingleGuest(singleGuests), ...processGroupGuests(groupGuests)];
};

export function useGuestManagement() {
  const guests: Ref<IGuest[] | null> = ref(null);
  const error: Ref<ErrorResponse | null> = ref(null);
  const isLoading = ref(true);

  const fetchGuests = async () => {
    isLoading.value = true;
    const { data, error: fetchError } = await fetchAllGuests();
    if (fetchError.value) {
      error.value = fetchError.value;
    } else {
      guests.value = data.value || [];
    }

    isLoading.value = false;
  };

  const computedGuests = computed<GuestManagement[]>(() => convertToGuestManagementList(guests.value || []).toSorted((a, b) => dateTimeAscendingComparer(a.createAt, b.createAt)));

  const refreshGuests = () => {
    fetchGuests();
  };

  // 立即執行首次獲取
  fetchGuests();

  return {
    guests: computedGuests,
    error,
    isLoading,
    refreshGuests
  };
}