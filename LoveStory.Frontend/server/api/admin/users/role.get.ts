const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const requestUrl = `${backendServiceBaseApiAddress}/api/users/role`;
  return await $fetch<string>(requestUrl, { method: "GET", headers: event.headers });
});