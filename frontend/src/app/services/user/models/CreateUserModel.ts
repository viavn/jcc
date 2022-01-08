import { UserType } from "../../auth/enums/UserType";

export interface CreateUserModel {
  name: string;
  login: string;
  password: string;
  userType: UserType;
}
