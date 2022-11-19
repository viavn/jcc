import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { SnackbarComponent } from 'src/app/components/snackbar/snackbar.component';
import { SystemNotification } from './models/SystemNotification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private readonly messageSubject = new Subject<SystemNotification>();
  message$ = this.messageSubject.asObservable();

  constructor(private _snackBar: MatSnackBar) { }

  emitMessage(notification: SystemNotification): void {
    this._snackBar.openFromComponent(SnackbarComponent, {
      duration: notification.ShowtimeInMilliseconds,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
      data: notification,
      panelClass: 'jcc_snackbar_panel'
    });
  }
}
