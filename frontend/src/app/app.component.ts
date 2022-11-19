import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NotificationService } from './services/notification/notification.service';
import { first, Subject, takeUntil, timer } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from './services/auth/auth.service';
import { MatSidenav } from '@angular/material/sidenav';
import { MenuItem } from './models/MenuItem';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'JCC App';
  homeLink = '/';
  showLogout = false;
  userIsAdmin = false;

  private menuItemsSubject = new Subject<MenuItem[]>();
  menuItems$ = this.menuItemsSubject.asObservable();

  private destroySubject = new Subject<void>();
  destroy$ = this.destroySubject.asObservable();

  @ViewChild('sidenav') sidenav!: MatSidenav;

  constructor(
    private notificationService: NotificationService,
    private router: Router,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.authService.loginRealized$
      .pipe(
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        try {
          this.showLogout = true;
          const user = this.authService.getUserInSessionStorage();
          this.userIsAdmin = user.isUserAdmin;
          this.loadMenuItems();

        } catch (error) {
          this.router.navigate(['/login']);
        }
      })

    if (this.authService.isUserInSessionStorage()) {
      this.authService.emitUserHasLoggedIn();
    }
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  logout(): void {
    this.sidenav.close();
    this.showLogout = false;
    this.authService.cleanAuthFromStorage();
    this.router.navigate(['/login']);
  }

  navigateTo(link: string): void {
    this.sidenav.close();
    this.router.navigate([link]);
  }

  private loadMenuItems() {
    timer(100).pipe(first())
      .subscribe(() => {
        let menuItems: MenuItem[] = [{
          text: 'Crianças',
          icon: 'face',
          link: '/',
        }];

        if (this.userIsAdmin) {
          menuItems = [
            ...menuItems,
            {
              text: 'Famílias',
              icon: 'family_restroom',
              link: '/families',
            },
            {
              text: 'Usuários',
              icon: 'manage_accounts',
              link: '/manage-accounts',
            }];
        }

        menuItems.push({
          text: 'Alterar senha',
          icon: 'settings',
          link: '/user',
        });
        this.menuItemsSubject.next(menuItems);
      });
  }
}
