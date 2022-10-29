import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TypeResponse } from '../child/models/Child';

type ResponseType = { data: TypeResponse[] };

@Injectable({
  providedIn: 'root'
})
export class GiftTypeService {
  private readonly RESOURCE_URL = `${environment.BASE_API_URL}api/v1/gift-types`;

  constructor(
    private http: HttpClient,
  ) { }

  getTypes(): Observable<TypeResponse[]> {
    const url = `${this.RESOURCE_URL}`;
    return this.http.get<ResponseType>(url).pipe(
      map(response => response.data),
    );
  }
}
