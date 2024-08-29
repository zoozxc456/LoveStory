import dayjs from "dayjs";

type WeddingGiftManagementState = {
  data: IWeddingGiftManagement[];
};

export type CreateWeddingGiftFormData = Pick<IWeddingGift, "amount" | "remark"> &
{ giftType: string; } &
  Pick<IWeddingGiftManagement, "managementName" | "managementType" | 'managementId'> &
  Pick<WeddingGiftManagementAttendance, "guestRelationship">;

export type DeleteWeddingGiftFormData = Pick<IWeddingGiftManagement, 'managementId' | 'managementType' | 'managementName'> &
  Pick<IWeddingGift, 'id'>;

export type ModifyWeddingGiftFormData = CreateWeddingGiftFormData & {
  weddingGiftId: string;
};

export const useWeddingGiftManagementStore = defineStore('wedding-gift-management', {
  state: (): WeddingGiftManagementState => ({ data: [] }),
  getters: {
    aggregateGifts: (state) => (({ managementId }: Pick<IWeddingGiftManagement, 'managementId'>): string => {
      const management = state.data.find(x => x.managementId === managementId);

      if (management !== undefined) {
        const availableGifts = management.attendance
          .map(({ weddingGift }) => weddingGift)
          .filter((weddingGift) => weddingGift !== undefined && weddingGift !== null);

        const groupedGifts = customGroupBy(availableGifts);
        return groupedGifts.map(({ key, value }) => {
          return ({
            key,
            value: value.filter(x => x.amount > 0).reduce((prev, curr) => prev + curr.amount, 0)
          });
        }).map(x => `${x.key}:${x.value}`).join(" / ");
      }
      return "";
    }),
    presentationReceivedAt: () => (data?: IWeddingGift) => data?.receivedAt === undefined ? "" : dayjs(data.receivedAt).format('YYYY-MM-DD HH:mm'),
    presentationGift: () => (data?: IWeddingGift) => data?.type === undefined || data?.amount === undefined ? "" : `${data?.type ?? ""} : ${data?.amount ?? ""}`,
    hasAnyWeddingGift: () => (data: WeddingGiftManagementAttendance[]) => data.some(attendance => attendance.weddingGift !== null)
  },

  actions: {
    async fetch() {
      const { data, error } = await useAsyncData<IWeddingGiftManagement[], ErrorResponse>('fetch-wedding-gift-management', () => $fetch<IWeddingGiftManagement[]>('/api/admin/wedding-gifts', { headers: generateJwtAuthorizeHeader() }));

      if (data.value !== null) this.data = data.value;
    },
    async refresh() {
      this.fetch();
    },
    async createWeddingGift(data: CreateWeddingGiftFormData, onCreated: () => void) {
      await useAsyncData('create-wedding-gift', () => $fetch<any>('/api/admin/wedding-gifts', { method: "POST", body: data, headers: generateJwtAuthorizeHeader() }));
      onCreated();
    },
    async deleteWeddingGift(data: DeleteWeddingGiftFormData, onDeleted: () => void) {
      await useAsyncData('delete-wedding-gift', () => $fetch<any>(`/api/admin/wedding-gifts/${data.id}`, { method: "DELETE", headers: generateJwtAuthorizeHeader() }));
      onDeleted();
    },
    async modifyWeddingGift(data: ModifyWeddingGiftFormData, onModified: () => void) {
      await useAsyncData('modify-wedding-gift', () => $fetch<any>('/api/admin/wedding-gifts', { method: "PUT", body: data, headers: generateJwtAuthorizeHeader() }));
      onModified();
    }
  }
});


const customGroupBy = (data: IWeddingGift[]) => {
  const keys = new Set(data.map(x => x.type));
  return [...keys.values()].map<{ key: string, value: IWeddingGift[]; }>(key => ({ key, value: data.filter(x => x.type === key) }));
};