import { UserType } from "../enums/UserType";

export class User {
  id: string;
  login: string;
  password: string;
  userType: UserType;

  constructor(id: string, login: string, password: string, userType: UserType) {
    this.id = id;
    this.login = login;
    this.password = password;
    this.userType = userType;
  }

  get isUserAdmin(): boolean {
    return this.userType === UserType.ADMIN
  }

}
