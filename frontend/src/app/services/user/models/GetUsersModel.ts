import { UserType } from "../../auth/enums/UserType";

export interface GetUsersModel {
  id: string;
  login: string;
  name: string;
  isDeleted: boolean;
  userType: UserType;
}
