import { OriginalAuthRequest, OriginalAuthResponse } from "~/types/apis/auth.type";

const { backendServiceBaseApiAddress } = useRuntimeConfig();

export default defineEventHandler(async (event) => {
  const result = await readBody<OriginalAuthRequest>(event);

  const requestUrl = `${backendServiceBaseApiAddress}/api/auth/origin`;
  const res = await $fetch<OriginalAuthResponse>(requestUrl, {
    method: "POST", body: result
  });

  return ({ ...res });
});