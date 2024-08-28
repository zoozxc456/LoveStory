const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const id = getRouterParam(event, 'id');

  const requestUrl = `${backendServiceBaseApiAddress}/api/Guest/${id}`;
  return await $fetch(requestUrl, { method: 'DELETE', headers: event.headers });
});