export type OriginalAuthRequest = {
  username: string;
  password: string;
};

export type OriginalAuthResponse = { isSuccess: true; accessToken: string; } | { isSuccess: false; accessToken: null; };
