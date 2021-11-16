import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Child } from './models/Child';

@Injectable({
  providedIn: 'root'
})
export class ChildService {
  private children: Child[] = [];

  constructor() {
    for (let i = 1; i <= 100; i++) {
      this.children.push({
        id: i.toString(),
        name: `Nome criança_${i}`,
        familyId: i.toString(),
        responsible: `Nome responsável_${i}`,
        phone: `(19) 981192732`,
        address: 'Rua Pirassununga, 743, Pq. Novo Mundo',
        age: `${i} anos`,
        clothesSize: 'M',
        shoeSize: '41',
      });
    }
  }

  getChildren(): Observable<Child[]> {
    return of(this.children);
  }

  getChild(id: string): Observable<Child | undefined> {
    const childIndex = this.children.findIndex(c => c.id === id);
    if (childIndex >= 0) {
      return of(this.children[childIndex]);
    }

    return of(undefined);
  }
}
