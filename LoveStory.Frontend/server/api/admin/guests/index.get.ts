const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const requestUrl = `${backendServiceBaseApiAddress}/api/Guest`;
  return await $fetch(requestUrl, { method: "GET", headers: event.headers });;
});