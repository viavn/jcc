import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';
import { CreateChildRequest, CreateGiftRequest, DashChildModel, GetChildrenByIdResponse } from './models/Child';

type GetChildrenResponse = { data: DashChildModel[] };
type GetByIdResponse = { data: GetChildrenByIdResponse };

@Injectable({
  providedIn: 'root'
})
export class ChildService {
  private readonly RESOURCE_URL = `${environment.BASE_API_URL}api/v1/children`;
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    private authService: AuthService,
  ) { }

  getChildren(): Observable<DashChildModel[]> {
    const url = `${this.RESOURCE_URL}`;
    return this.http.get<GetChildrenResponse>(url).pipe(
      map(response => response.data),
    );
  }

  getChild(id: string): Observable<GetChildrenByIdResponse> {
    const url = `${this.RESOURCE_URL}/${id}`;
    return this.http.get<GetByIdResponse>(url).pipe(
      map(response => response.data),
    );
  }

  getChildrenReport(): Observable<any> {
    const url = `${this.RESOURCE_URL}/export`;
    return this.http.get(url, {
      responseType: 'blob'
    });
  }

  create(request: Partial<CreateChildRequest>): Observable<any> {
    const url = `${this.RESOURCE_URL}`;
    return this.http.post<any>(url, request, this.httpOptions);
  }

  update(request: Partial<CreateChildRequest>): Observable<any> {
    const url = `${this.RESOURCE_URL}/${request.id}`;
    return this.http.put<any>(url, request, this.httpOptions);
  }

  delete(id: string): Observable<any> {
    const url = `${this.RESOURCE_URL}/${id}`;
    return this.http.delete<any>(url, this.httpOptions);
  }

  deliverGift(childId: string, godParentId: string, gifTypeId: number): Observable<any> {
    const url = `${this.RESOURCE_URL}/${childId}/gifts/${gifTypeId}`;
    return this.http.patch<any>(url, { godParentId }, this.httpOptions);
  }

  addGift(request: CreateGiftRequest): Observable<any> {
    const user = this.authService.getUserInSessionStorage();
    if (!user) {
      return throwError(() => 'Usuário não encontrado');
    }

    const url = `${this.RESOURCE_URL}/${request.childId}/gifts`;
    const bodyRequest: any = {
      userId: user.id,
      type: request.typeId,
      godParent: {
        name: request.godParent.name,
        contactNumber: request.godParent.contactNumber,
        address: request.godParent.address
      }
    };
    return this.http.post<any>(url, bodyRequest, this.httpOptions);
  }

  removeGift(childId: string, godParentId: string, giftTypeId: number): Observable<any> {
    const url = `${this.RESOURCE_URL}/${childId}/god-parents/${godParentId}/gifts/${giftTypeId}`;
    return this.http.delete<any>(url, this.httpOptions);
  }
}
