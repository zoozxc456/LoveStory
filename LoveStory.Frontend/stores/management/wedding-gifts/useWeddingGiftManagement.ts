import dayjs from "dayjs";

type WeddingGiftManagementState = {
  data: IWeddingGiftManagement[];
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
      const { data, error } = await useAsyncData<IWeddingGiftManagement[], ErrorResponse>('fetch-wedding-gift-management', () => $fetch<IWeddingGiftManagement[]>('/api/admin/wedding-gifts'));

      if (data.value !== null) this.data = data.value;
    }
  }
});


const customGroupBy = (data: IWeddingGift[]) => {
  const keys = new Set(data.map(x => x.type));
  return [...keys.values()].map<{ key: string, value: IWeddingGift[]; }>(key => ({ key, value: data.filter(x => x.type === key) }));
};