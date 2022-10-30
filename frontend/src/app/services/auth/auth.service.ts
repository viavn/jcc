import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SessionStorageService } from '../sessionStorage/session-storage.service';
import { UserType } from './enums/UserType';
import { User } from './models/User';
import { UserLoginModel } from './models/UserLoginModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly sessionStorageKey = '_auth';
  private readonly RESOURCE_URL = `${environment.BASE_API_URL}api/v1/users`;
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  private readonly loginRealized = new Subject<boolean>();
  loginRealized$ = this.loginRealized.asObservable();

  constructor(
    private http: HttpClient,
    private sessionStorageService: SessionStorageService,
  ) { }

  login(user: UserLoginModel) {
    const url = `${this.RESOURCE_URL}/login`;
    const bodyRequest: any = { ...user };
    return this.http.post<any>(url, bodyRequest, this.httpOptions);
  }

  saveUserToSessionStorage(user: User): void {
    const sessionUser = new User(user.id, user.login, '', user.userType);
    this.sessionStorageService.save(this.sessionStorageKey, JSON.stringify(sessionUser));
  }

  isUserInSessionStorage(): boolean {
    const sessionItem = this.sessionStorageService.getItem(this.sessionStorageKey);
    return sessionItem && JSON.parse(sessionItem);
  }

  isUserAdmin(): boolean {
    const user = this.getUserInSessionStorage();
    if (!user) {
      return false
    }

    return user.userType === UserType.ADMIN;
  }

  getUserInSessionStorage(): User {
    const sessionItem = this.sessionStorageService.getItem(this.sessionStorageKey);
    if (sessionItem) {
      const sessionObj = JSON.parse(sessionItem);
      return new User(sessionObj.id, sessionObj.login, sessionObj.password, sessionObj.userType);
    }
    throw new Error('Dados do session storage não encontrados');
  }

  cleanAuthFromStorage() {
    this.sessionStorageService.remove(this.sessionStorageKey);
  }

  getSessionStorageKey() {
    return this.sessionStorageKey;
  }

  emitUserHasLoggedIn(): void {
    this.loginRealized.next(true);
  }
}
