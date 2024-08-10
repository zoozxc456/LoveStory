export const useBanquetTable = () => {
  const tables = ref<IBanquetTable[]>([]);

  const fetchAll = async () => {
    const { data, error } = await fetchAllBanquetTables();
    if (error.value === null && data.value !== null) {
      tables.value = data.value;
    }
  };

  fetchAll();

  return {
    data: computed(() => tables.value)
  };
};
