const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const requestUrl = `${backendServiceBaseApiAddress}/api/recipient/guest-arrive`;
  const payload = await readBody<{ guestId: string; guestType: 'single' | 'family'; }>(event);
  const res = await $fetch<{ isSuccess: boolean; }>(requestUrl, { method: "PATCH", body: payload, headers: event.headers });

  return res.isSuccess;
});
