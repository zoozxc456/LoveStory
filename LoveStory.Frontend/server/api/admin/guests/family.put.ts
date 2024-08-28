import { ModifyFamilyGuestRequest } from "~/types/apis/guest.type";

const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const body = await readBody<ModifyFamilyGuestRequest>(event);
  const requestUrl = `${backendServiceBaseApiAddress}/api/guest/family`;

  return await $fetch(requestUrl, { method: "PUT", headers: event.headers, body });
});