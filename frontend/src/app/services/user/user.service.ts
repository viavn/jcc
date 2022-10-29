import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateUserModel } from './models/CreateUserModel';
import { GetUsersModel } from './models/GetUsersModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly RESOURCE_URL = `${environment.BASE_API_URL}api/v1/users`;
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
  ) { }

  changeUserPassword(id: string, newPassword: string): Observable<any> {
    const url = `${this.RESOURCE_URL}/${id}/password`;
    const bodyRequest: any = { newpassword: newPassword };
    return this.http.patch<any>(url, bodyRequest, this.httpOptions);
  }

  createUser(user: CreateUserModel): Observable<any> {
    const url = `${this.RESOURCE_URL}`;
    const bodyRequest: any = {
      login: user.login,
      name: user.name,
      password: user.password,
      userType: user.userType
    };
    return this.http.post<any>(url, bodyRequest, this.httpOptions);
  }

  disableUser(id: string): Observable<any> {
    const url = `${this.RESOURCE_URL}/${id}`;
    return this.http.delete(url, this.httpOptions);
  }

  getUsers(): Observable<GetUsersModel[]> {
    const url = `${this.RESOURCE_URL}`;
    return this.http.get<any>(url)
      .pipe(map(request => {
        return request.data.map((u: any) => ({
          id: u.id,
          login: u.login,
          name: u.name,
          isDeleted: u.isDeleted,
          userType: u.userType,
        } as GetUsersModel));
      }));
  }

  getUserById(id: string): Observable<GetUsersModel> {
    const url = `${this.RESOURCE_URL}/${id}`;
    return this.http.get<any>(url)
      .pipe(map(({ data }) => ({
        id: data.id,
        login: data.login,
        name: data.name,
        isDeleted: data.isDeleted,
        userType: data.userType,
      } as GetUsersModel
      )));
  }
}
