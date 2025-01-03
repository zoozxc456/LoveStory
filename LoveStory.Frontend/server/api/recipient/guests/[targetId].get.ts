const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const targetId = getRouterParam(event, 'targetId');
  const { guestType } = getQuery(event) as { guestType: string; };

  const requestUrl = `${backendServiceBaseApiAddress}/api/Recipient/guests/${targetId}`;
  return await $fetch(requestUrl, { method: "GET", headers: event.headers, query: { guestType } });;
});