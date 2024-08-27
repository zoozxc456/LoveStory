import { UserManagement } from "~/types/management/users/user-management.type";

const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const { userId } = await readBody<Pick<UserManagement, "userId">>(event);
  const requestUrl = `${backendServiceBaseApiAddress}/api/users/${userId}/Password/Reset`;

  return await $fetch(requestUrl, { method: "PATCH", headers: event.headers });
});