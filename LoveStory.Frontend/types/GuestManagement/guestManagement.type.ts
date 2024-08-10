// Guest Management Detail Types
export type SingleGuestManagementDetail = Omit<IGuest, 'guestRelationship' | 'guestGroup'>;
export type GroupGuestManagementDetail =  Omit<IGuest, 'guestRelationship'>;

// Guest Management Types
type BasicGuestManagement = Pick<IGuest, 'guestId' | 'guestRelationship' | 'createAt' | 'creator' | 'guestName'>;

export type SingleGuestManagement = BasicGuestManagement & { guestType: 'single', details: SingleGuestManagementDetail[]; };
export type GroupGuestManagement = BasicGuestManagement & { guestType: 'group', details: GroupGuestManagementDetail[]; };
export type GuestManagement = SingleGuestManagement | GroupGuestManagement;