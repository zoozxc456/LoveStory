const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const requestUrl = `${backendServiceBaseApiAddress}/api/users`;
  return await $fetch(requestUrl, { method: "GET", headers: event.headers });;
});