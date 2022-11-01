import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseFamilyRequest, CreateFamilyRequest, FamilyByIdResponse, FamilyResponse, MemberRequest } from './models/FamilyModels';

type GetFamiliesResponse = { data: FamilyResponse[] };
type GetFamilyByIdResponse = { data: FamilyByIdResponse };

@Injectable({
  providedIn: 'root'
})
export class FamilyService {
  private readonly RESOURCE_URL = `${environment.BASE_API_URL}api/v1/families`;
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
  ) { }

  getFamilies(): Observable<FamilyResponse[]> {
    const url = `${this.RESOURCE_URL}`;
    return this.http.get<GetFamiliesResponse>(url).pipe(
      map(response => response.data),
    );
  }

  getFamily(id: string): Observable<FamilyByIdResponse> {
    const url = `${this.RESOURCE_URL}/${id}`;
    return this.http.get<GetFamilyByIdResponse>(url).pipe(
      map(response => response.data),
    );
  }

  create(request: CreateFamilyRequest): Observable<any> {
    const url = `${this.RESOURCE_URL}`;
    return this.http.post<any>(url, request, this.httpOptions);
  }

  update(id: string, request: BaseFamilyRequest): Observable<any> {
    const url = `${this.RESOURCE_URL}/${id}`;
    return this.http.put<any>(url, request, this.httpOptions);
  }

  delete(id: string): Observable<any> {
    const url = `${this.RESOURCE_URL}/${id}`;
    return this.http.delete<any>(url, this.httpOptions);
  }

  createMember(familyId: string, request: MemberRequest): Observable<any> {
    const url = `${this.RESOURCE_URL}/${familyId}/members`;
    return this.http.post<any>(url, request, this.httpOptions);
  }

  updateMember(familyId: string, request: MemberRequest): Observable<any> {
    const url = `${this.RESOURCE_URL}/${familyId}/members/${request.id}`;
    return this.http.put<any>(url, request, this.httpOptions);
  }

  deleteMember(familyId: string, memberId: string): Observable<any> {
    const url = `${this.RESOURCE_URL}/${familyId}/${memberId}`;
    return this.http.delete<any>(url, this.httpOptions);
  }
}
