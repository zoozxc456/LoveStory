import type { CookieOptions } from "nuxt/app";

const ACCESSTOKEN_COOKIE_KEY = "accessToken";
const DEFAULT_COOKIE_MAX_AGE = 60 * 30;
const DEFAULT_COOKIE_OPTIONS: CookieOptions<string> & { readonly?: false; } = { maxAge: DEFAULT_COOKIE_MAX_AGE };

export const setAccessToken: (token: string, options?: CookieOptions<string> & { readonly?: false; }) => void = (token, options?) => {
  if (options === undefined) options = DEFAULT_COOKIE_OPTIONS;
  useCookie(ACCESSTOKEN_COOKIE_KEY, options).value = token;
};

export const getAccessToken = () => useCookie(ACCESSTOKEN_COOKIE_KEY).value;
export const hasAccessToken = () => !!useCookie(ACCESSTOKEN_COOKIE_KEY).value;
export const removeAccessToken = () => useCookie(ACCESSTOKEN_COOKIE_KEY).value = null;