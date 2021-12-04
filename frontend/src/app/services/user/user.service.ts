import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';

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
    private authService: AuthService,
  ) { }

  changeUserPassword(userId: string, newPassword: string): Observable<any> {
    const url = `${this.RESOURCE_URL}/${userId}/password`;
    const bodyRequest: any = { newpassword: newPassword };
    return this.http.patch<any>(url, bodyRequest, this.httpOptions);
  }
}
