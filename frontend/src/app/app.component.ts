import { Component, OnDestroy, OnInit } from '@angular/core';
import { NotificationService } from './services/notification/notification.service';
import { SystemNotification } from './services/notification/models/SystemNotification';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from './services/auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'JCC App';
  homeLink = '/';
  notification!: SystemNotification;
  showLogout = false;

  private notificationSubject = new Subscription();
  private loginSubject = new Subscription();

  constructor(
    private notificationService: NotificationService,
    private router: Router,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.showLogout = this.authService.isUserInSessionStorage();

    const messageSubject = this.notificationService.message$
      .subscribe(message => {
        this.notification = message;
      });
    this.notificationSubject.add(messageSubject);

    const loginSubject = this.authService.loginRealized$
      .subscribe(value => {
        this.showLogout = value;
      })
    this.loginSubject.add(loginSubject);
  }

  ngOnDestroy(): void {
    this.notificationSubject.unsubscribe();
    this.loginSubject.unsubscribe();
  }

  logout(): void {
    this.showLogout = false;
    this.authService.cleanAuthFromStorage();
    this.router.navigate(['/login']);
  }

  userSettings(): void {
    this.router.navigate(['/user']);
  }
}
