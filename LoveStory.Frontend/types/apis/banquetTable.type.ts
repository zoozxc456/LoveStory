import type { IUser } from "./user.type";

export interface IBanquetTable {
  banquetTableId: string;
  maxSeatAmount: number;
  minSeatAmount: number;
  tableAlias: string;
  remark: string;
  createAt: Date;
  creator: IUser;
}