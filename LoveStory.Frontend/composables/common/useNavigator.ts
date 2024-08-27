type NameItemType = {
  to: string;
  displayText: string;
  icon: string;
};

export const useNavigator = () => {
  const paths = reactive<NameItemType[]>([
    { to: "/", displayText: "總覽", icon: "house-chimney-window" },
    { to: "/gifts", displayText: "禮金管理", icon: "gift" },
    { to: "/guests", displayText: "賓客名單", icon: "people-group" },
  ]);

  const handleLogout = () => {
    removeAccessToken();
    navigateTo("/login");
  };

  return ({ paths, handleLogout });
};