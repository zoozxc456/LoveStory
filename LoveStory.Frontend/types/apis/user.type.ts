export interface IUser {
  userId: string;
  username: string;
}

export interface IUserManagement {
  userId: string;
  username: string;
  role: string;
  createAt: Date;
  creator?: IUserManagement;
}