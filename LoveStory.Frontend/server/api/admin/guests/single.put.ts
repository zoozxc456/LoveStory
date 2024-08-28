import { ModifySingleGuestRequest } from "~/types/apis/guest.type";

const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const body = await readBody<ModifySingleGuestRequest>(event);
  const requestUrl = `${backendServiceBaseApiAddress}/api/Guest`;
  return await $fetch(requestUrl, { method: "PUT", headers: event.headers, body });
});