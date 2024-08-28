import { UserManagement } from "~/types/management/users/user-management.type";

const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const body = await readBody<Pick<UserManagement, 'userId' | 'username' | 'role'>>(event);
  const requestUrl = `${backendServiceBaseApiAddress}/api/users/${body.userId}`;

  return await $fetch(requestUrl, { method: "PUT", headers: event.headers, body });
});