import { Component, Inject } from '@angular/core';
import { MatSnackBarRef, MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';
import { SystemNotification, NotificationType } from '../../services/notification/models/SystemNotification'

@Component({
  selector: 'app-snackbar',
  templateUrl: './snackbar.component.html',
  styleUrls: ['./snackbar.component.scss']
})
export class SnackbarComponent {

  constructor(
    private _snackbarRef: MatSnackBarRef<SnackbarComponent>,
    @Inject(MAT_SNACK_BAR_DATA) public data: SystemNotification,
  ) { }

  private readonly notificationTypeMap = new Map<NotificationType, string>([
    [NotificationType.ERROR, 'report_problem'],
    [NotificationType.WARN, 'info'],
    [NotificationType.INFO, 'check_circle'],
  ]);

  private readonly notificationTypeCssMap = new Map<NotificationType, string>([
    [NotificationType.ERROR, 'error'],
    [NotificationType.WARN, 'warning'],
    [NotificationType.INFO, 'informartion'],
  ]);

  close() {
    this._snackbarRef.dismiss();
  }

  get notificationText() {
    return this.notificationTypeMap.get(this.data.Type) || '';
  }

  get getCssClassByNotificationType() {
    return this.notificationTypeCssMap.get(this.data.Type) || '';
  }
}
