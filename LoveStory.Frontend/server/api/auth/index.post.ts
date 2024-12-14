import { OriginalAuthRequest, OriginalAuthResponse } from "~/types/apis/auth.type";

const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const result = await readBody<OriginalAuthRequest>(event);

  const requestUrl = `${backendServiceBaseApiAddress}/api/auth/origin`;
  const res = await $fetch<OriginalAuthResponse>(requestUrl, {
    method: "POST", body: result
  });

  if (res.isSuccess === true) {
    const role = await requestRole(res.accessToken);
    if (role === 'admin') res.path = '/management';
    else res.path = '/';
  }
  return ({ ...res });
});

const requestRole = async (accessToken: string) => {
  const requestUrl = `${backendServiceBaseApiAddress}/api/users/role`;
  return await $fetch<string>(requestUrl, {
    method: "GET",
    headers: { Authorization: `Bearer ${accessToken}` }
  });
};