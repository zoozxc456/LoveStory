const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const userId = getRouterParam(event, 'userId');

  const requestUrl = `${backendServiceBaseApiAddress}/api/users/${userId}`;
  return await $fetch(requestUrl, { method: 'DELETE', headers: event.headers });
});