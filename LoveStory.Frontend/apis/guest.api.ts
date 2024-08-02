const fetchAllGuests = async () => useFetch<GetGuestManagementResponse, ErrorResponse>("http://localhost:5066/api/Guest", {
  server: true,
  lazy: false,
  retry: 3,
  onResponseError({ response }) {
    console.error('API error:', response._data);
  }
});

const addGuest = async (data: AddGuestManagementRequest) => useFetch<any, ErrorResponse>('http://localhost:5066/api/Guest', { method: 'POST', body: data });
const addFamilyGuest = async (data: AddFamilyGuestRequest) => useFetch<any, ErrorResponse>('http://localhost:5066/api/Guest/Family', { method: 'POST', body: data });
const deleteGuest = async (data: Pick<IGuest, 'guestId'>) => useAsyncData<any, ErrorResponse>(`http://localhost:5066/api/Guest/${data}`, () => $fetch(`http://localhost:5066/api/Guest/${data}`, { method: 'DELETE' }));
export { fetchAllGuests, addGuest, addFamilyGuest, deleteGuest };