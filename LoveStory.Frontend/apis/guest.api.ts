import type { GetGuestManagementResponse, ModifyFamilyGuestRequest, ModifySingleGuestRequest } from "types/apis/guest.type";

const fetchAllGuests = async () => useAsyncData<GetGuestManagementResponse, ErrorResponse>("http://localhost:5066/api/Guest", () => $fetch<GetGuestManagementResponse>("http://localhost:5066/api/Guest"), { server: true, lazy: false });

const addGuest = async (data: AddGuestManagementRequest) => useAsyncData<any, ErrorResponse>('http://localhost:5066/api/Guest', () => $fetch('http://localhost:5066/api/Guest', { method: 'POST', body: data }));
const addFamilyGuest = async (data: AddFamilyGuestRequest) => useAsyncData<any, ErrorResponse>('http://localhost:5066/api/Guest/Family', () => $fetch('http://localhost:5066/api/Guest/Family', { method: 'POST', body: data }));
const deleteGuest = async (data: Pick<IGuest, 'guestId'>) => useAsyncData<any, ErrorResponse>(`http://localhost:5066/api/Guest/${data}`, () => $fetch(`http://localhost:5066/api/Guest/${data}`, { method: 'DELETE' }));

const modifySingleGuest = async (data: ModifySingleGuestRequest) => useAsyncData<any, ErrorResponse>('http://localhost:5066/api/Guest', () => $fetch('http://localhost:5066/api/Guest', { method: 'PUT', body: data }));
const modifyFamilyGuest = async (data: ModifyFamilyGuestRequest) => useAsyncData<any, ErrorResponse>('http://localhost:5066/api/Guest/Family', () => $fetch('http://localhost:5066/api/Guest/Family', { method: 'PUT', body: data }));
export { fetchAllGuests, addGuest, addFamilyGuest, deleteGuest, modifySingleGuest, modifyFamilyGuest };