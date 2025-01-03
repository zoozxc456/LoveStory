export interface RecipientGuest {
  targetId: string;
  guestName: string;
  attendanceAmount: number;
  relationship: string;
  arrivedAt?: Date;
}

export type RecipientGuestOverview = RecipientGuest & {
  specialNeed: string[];
  remark?: string;
  seatLocation?: string;
};