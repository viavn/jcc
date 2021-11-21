import { Component, Input, OnDestroy } from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { SystemNotification, NotificationType } from '../../services/notification/models/SystemNotification'

@Component({
  selector: 'app-snackbar',
  templateUrl: './snackbar.component.html',
  styleUrls: ['./snackbar.component.scss']
})
export class SnackbarComponent implements OnDestroy {

  private subject = new Subscription();

  systemNotification: SystemNotification = {
    Message: '',
    ShowNotification: false,
    ShowtimeInMilliseconds: 0,
    Type: NotificationType.INFO,
  };

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

  @Input()
  set notification(value: SystemNotification | undefined) {
    if (value && value.ShowNotification) {
      this.systemNotification = { ...value };

      const timerSubscription = timer(this.systemNotification.ShowtimeInMilliseconds)
        .subscribe(
          () => {
            this.systemNotification.ShowNotification = false;
            this.systemNotification.ShowtimeInMilliseconds = 0;
          });

      this.subject.add(timerSubscription);
    }
  }

  ngOnDestroy(): void {
    this.subject.unsubscribe();
  }

  close() {
    this.systemNotification.ShowNotification = false;
  }

  get notificationText() {
    return this.notificationTypeMap.get(this.systemNotification.Type) || '';
  }

  get getCssClassByNotificationType() {
    return this.notificationTypeCssMap.get(this.systemNotification.Type) || '';
  }

}
