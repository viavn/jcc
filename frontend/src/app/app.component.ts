import { Component, OnDestroy, OnInit } from '@angular/core';
import { NotificationService } from './services/notification/notification.service';
import { Subject, takeUntil } from 'rxjs';
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
  showLogout = false;
  showManageAccounts = false;

  private destroySubject = new Subject<void>();
  destroy$ = this.destroySubject.asObservable();

  notification$ = this.notificationService.message$;

  constructor(
    private notificationService: NotificationService,
    private router: Router,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.showLogout = this.authService.isUserInSessionStorage();

    this.authService.loginRealized$
      .pipe(takeUntil(this.destroy$))
      .subscribe(value => {
        this.showLogout = value;

        const user = this.authService.getUserInSessionStorage();
        this.showManageAccounts = user.isUserAdmin;
      })
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  logout(): void {
    this.showLogout = false;
    this.authService.cleanAuthFromStorage();
    this.router.navigate(['/login']);
  }

  userSettings(): void {
    this.router.navigate(['/user']);
  }

  manageAccounts(): void {
    this.router.navigate(['/manage-accounts']);
  }

  families(): void {
    this.router.navigate(['/families']);
  }
}
