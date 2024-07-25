import type { GetGuestManagementResponse } from "types/apis/guest.type";
import type { ErrorResponse } from "types/apis/index.type";

const fetchAllGuests = async () => useFetch<GetGuestManagementResponse, ErrorResponse>("http://localhost:5066/api/Guest", {
  server: true,
  lazy: false,
  retry: 3,
  onResponseError({ response }) {
    console.error('API error:', response._data);
  }
});

export { fetchAllGuests };