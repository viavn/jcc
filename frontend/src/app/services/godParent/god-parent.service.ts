import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GodParentResponse, TypeResponse } from '../child/models/Child';

@Injectable({
  providedIn: 'root'
})
export class GodParentService {
  private readonly RESOURCE_URL = `${environment.BASE_API_URL}api/v1/god-parents`;
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
  ) { }

  update(godParent: GodParentResponse): Observable<TypeResponse[]> {
    const url = `${this.RESOURCE_URL}/${godParent.id}`;
    const bodyRequest = {
      name: godParent.name,
      contactNumber: godParent.contactNumber,
      address: godParent.address,
    };
    return this.http.put<any>(url, bodyRequest, this.httpOptions);
  }
}
