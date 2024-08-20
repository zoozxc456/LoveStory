import type { GetGuestManagementResponse, ModifyFamilyGuestRequest, ModifySingleGuestRequest } from "types/apis/guest.type";
const generateJwtAuthorizationHeader = () => {
  return ({ Authorization: `Bearer ${getAccessToken()}` });
};

const modifySingleGuest = async (data: ModifySingleGuestRequest) => useAsyncData<any, ErrorResponse>('http://localhost:5066/api/Guest', () => $fetch('http://localhost:5066/api/Guest', { method: 'PUT', body: data, headers: { ...generateJwtAuthorizationHeader() } }));
const modifyFamilyGuest = async (data: ModifyFamilyGuestRequest) => useAsyncData<any, ErrorResponse>('http://localhost:5066/api/Guest/Family', () => $fetch('http://localhost:5066/api/Guest/Family', { method: 'PUT', body: data, headers: { ...generateJwtAuthorizationHeader() } }));
export { modifySingleGuest, modifyFamilyGuest };