const originalAuthRequest = async (data: OriginalAuthRequest) => useFetch<OriginalAuthResponse, ErrorResponse>('/api/auth', { method: "POST", body: data });

export const useOriginalAuth = () => {
  const authFormData = reactive<OriginalAuthRequest>({
    username: "",
    password: ""
  });

  const errorMessage = ref<string>("");
  const isLoading = ref<boolean>(false);

  const authenticateUser = async () => {
    isLoading.value = true;
    const { data } = await originalAuthRequest(authFormData);
    if (data !== null && data.value !== null && data.value.isSuccess) {
      setAccessToken(data.value.accessToken);
      navigateTo({ path: '/' });
    } else {
      errorMessage.value = "Username or Password Is incorrect.";
    }

    isLoading.value = false;
  };

  return { authFormData, authenticateUser, errorMessage, isLoading };
};