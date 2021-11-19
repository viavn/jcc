import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, EMPTY, map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
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
    private http: HttpClient
  ) {

  }

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
      catchError(this.handleError<DashChildModel[]>(`getChildren`))
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
      catchError(this.handleError<Child>(`getChild`))
    );
  }

  addOrUpdateChildGodParents(childId: string, godParents: GodParent[]): Observable<any> {
    const url = `${this.RESOURCE_URL}/${childId}`;
    const bodyRequest: any = { godParents: godParents };

    return this.http.patch<any>(url, bodyRequest, this.httpOptions).pipe(
      catchError(this.handleError<any>('addOrUpdateChildGodParents'))
    );
  }

  /**
 * Handle Http operation that failed.
 * Let the app continue.
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
  */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
