import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { SystemNotification } from './models/SystemNotification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private readonly messageSubject = new Subject<SystemNotification>();
  message$ = this.messageSubject.asObservable();

  emitMessage(notification: SystemNotification): void {
    this.messageSubject.next(notification);
  }
}
