const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const body = await readBody<{ seatLocationId: string; guestId: string; }>(event);

  const requestUrl = `${backendServiceBaseApiAddress}/api/GuestManagement/SeatLocation-Guests/${body.guestId}`;

  return await $fetch(requestUrl, { method: "PATCH", headers: event.headers, body });
});