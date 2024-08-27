const fetchAllUsersRequest = () => useAsyncData<IUserManagement[], ErrorResponse>('fetch-all-users', () => $fetch<IUserManagement[]>('/api/admin/users', { method: "GET", headers: generateJwtAuthorizeHeader() }));


type UserManagementState = {
  data: UserManagement[];
  isLoading: boolean;
};

export const useUserManagementStore = defineStore("user-management", {
  state: (): UserManagementState => ({
    data: [],
    isLoading: true
  }),
  getters: {
    users: (state): UserManagement[] => state.data
  },
  actions: {
    async fetchAllUsers() {
      const { data, error } = await fetchAllUsersRequest();

      if (error.value === null && data.value !== null) {
        this.data = data.value;
      }
    },
    async refreshUsers() {
      this.fetchAllUsers();
    },

    async createUser(data: Pick<UserManagement, 'username' | 'role'>, onCreated: () => void) {
      if (data.role === "" || data.username === "") throw "Create User is not completed";
      await useAsyncData('create-user', () => $fetch<any>('/api/admin/users', { body: data, method: "POST", headers: generateJwtAuthorizeHeader() }));
      onCreated();
    },
    async modifyUserBasicInfo(data: Pick<UserManagement, 'userId' | 'username' | 'role'>, onModified: () => void) {
      if (data.role === "" || data.username === "") throw "Modify User Basic Info is not completed";
      await useAsyncData('modify-user-basic-info', () => $fetch<any>('/api/admin/users', { body: data, method: "PUT", headers: generateJwtAuthorizeHeader() }));
      onModified();
    }
  }
});