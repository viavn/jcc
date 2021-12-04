import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';
import { Child, DashChildModel, GodParent } from './models/Child';

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
    return this.http.get<any[]>(url).pipe(
      map(dataFromApi => {
        return dataFromApi.map(item => {
          const dashChildModel: DashChildModel = {
            id: item.id,
            name: item.name,
            legalResponsible: item.legalResponsible,
            familyAcronym: item.familyAcronym
          };

          return dashChildModel
        });
      }),
    );
  }

  getChild(id: string): Observable<Child> {
    const url = `${this.RESOURCE_URL}/${id}`;
    return this.http.get<any>(url).pipe(
      map(dataFromApi => {
        const child: Child = {
          id: dataFromApi.id,
          name: dataFromApi.name,
          age: dataFromApi.age,
          clothesSize: dataFromApi.clothesSize,
          shoeSize: dataFromApi.shoeSize,
          legalResponsible: dataFromApi.legalResponsible,
          familyAcronym: dataFromApi.familyAcronym,
          familyPhone: dataFromApi.familyPhone,
          familyAddress: dataFromApi.familyAddress,
          godParents: dataFromApi.godParents.map((gp: any) => {
            const godParent: GodParent = {
              id: gp.id,
              name: gp.name,
              phone: gp.phone,
              isClothesSelected: gp.isClothesSelected,
              isShoeSelected: gp.isShoeSelected,
              isGiftSelected: gp.isGiftSelected,
            };
            return godParent;
          }),
        };
        return child;
      }),
    );
  }

  getChildrenReport(): Observable<any> {
    const url = `${this.RESOURCE_URL}/export`;
    return this.http.get(url, {
      responseType: 'blob'
    });
  }

  addOrUpdateChildGodParents(childId: string, godParents: GodParent[]): Observable<any> {
    const user = this.authService.getUserInSessionStorage();
    if (!user) {
      return throwError(() => 'Usuário não encontrado no session storage');
    }

    const url = `${this.RESOURCE_URL}/${childId}`;
    const bodyRequest: any = { userLogin: user.login, godParents: godParents };
    return this.http.patch<any>(url, bodyRequest, this.httpOptions);
  }
}
