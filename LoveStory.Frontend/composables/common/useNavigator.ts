type NameItemType = {
  to: string;
  displayText: string;
  icon: string;
};

export const useNavigator = () => {
  const pathTable = reactive<{ [key in "admin" | "recipient"]: NameItemType[] }>({
    'admin': [{ to: "/management/", displayText: "總覽", icon: "house-chimney-window" },
    { to: "/management/gifts", displayText: "禮金管理", icon: "gift" },
    { to: "/management/guests", displayText: "賓客名單", icon: "people-group" },
    { to: "/management/users", displayText: "帳號管理", icon: "people-group" },],
    recipient: [{
      to: "/",
      displayText: "總覽",
      icon: "house-chimney-window"
    }]
  });

  const state = ref<NameItemType[]>([]);

  const getRole = async () => {
    const { data } = await useAsyncData<string>('fetch-user-role', () => $fetch('/api/admin/users/role', { method: "GET", headers: generateJwtAuthorizeHeader() }));
    return data.value;
  };

  const fetchPaths = async () => {
    const role = await getRole();
    if (role === null) return state.value = [];
    else if (role === "admin") return state.value = pathTable.admin;
    return state.value = pathTable.recipient;
  };

  fetchPaths();

  const handleLogout = () => {
    removeAccessToken();
    navigateTo("/login");
  };

  return ({ paths: state, handleLogout });
};