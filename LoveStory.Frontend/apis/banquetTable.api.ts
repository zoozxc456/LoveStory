const fetchAllBanquetTables = async () => useAsyncData<IBanquetTable[], ErrorResponse>('http://localhost:5066/api/GuestManagement/SeatLocation', () => $fetch<IBanquetTable[]>('http://localhost:5066/api/GuestManagement/SeatLocation'), {
  server: true,
  lazy: false,
});

export { fetchAllBanquetTables };