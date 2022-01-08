import { UserType } from "../enums/UserType";

export interface User {
  id: string;
  login: string;
  password: string;
  userType: UserType;
}
