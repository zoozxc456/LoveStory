const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const body = await readBody(event);
  const requestUrl = `${backendServiceBaseApiAddress}/api/guest`;

  return await $fetch(requestUrl, { method: "POST", headers: event.headers, body });
});