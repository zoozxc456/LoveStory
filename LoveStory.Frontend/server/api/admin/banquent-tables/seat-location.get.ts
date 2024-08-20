const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const requestUrl = `${backendServiceBaseApiAddress}/Api/GuestManagement/SeatLocation`;
  return await $fetch(requestUrl, { method: "GET", headers: event.headers });
});