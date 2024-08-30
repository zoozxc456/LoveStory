const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const body = await readBody(event);
  const requestUrl = `${backendServiceBaseApiAddress}/api/WeddingGiftManagement`;

  return await $fetch(requestUrl, { method: "PUT", headers: event.headers, body });
});