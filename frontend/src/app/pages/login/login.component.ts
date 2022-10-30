import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { AuthService } from 'src/app/services/auth/auth.service';
import { User } from 'src/app/services/auth/models/User';
import { UserLoginModel } from 'src/app/services/auth/models/UserLoginModel';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnDestroy {
  loginFormGroup: FormGroup;
  hidePassword = true;

  private destroySubject = new Subject<void>();
  destroy$ = this.destroySubject.asObservable();

  constructor(
    private fb: FormBuilder,
    private router: Router,
    public dialog: MatDialog,
    private notificationService: NotificationService,
    private authService: AuthService,

  ) {
    this.loginFormGroup = this.createLoginFormGroup();
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  authenticate() {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    const loginFormValues = this.loginFormGroup.value;
    const loginModel: UserLoginModel = {
      login: loginFormValues.user,
      password: loginFormValues.password,
    };

    this.authService.login(loginModel)
      .pipe(
        takeUntil(this.destroy$),
        catchError(() => {
          spinnerDialogRef.close();
          this.notificationService.emitMessage({
            Message: 'Usuário ou senhas inválidos tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
          return EMPTY;
        }))
      .subscribe(
        (userInfo: any) => {
          const user = new User(userInfo.id, userInfo.login, '', userInfo.userType);
          this.authService.saveUserToSessionStorage(user);
          spinnerDialogRef.close();
          this.authService.emitUserHasLoggedIn();
          this.router.navigate(['/']);
        });
  }

  private createLoginFormGroup(): FormGroup {
    return this.fb.group({
      user: [null, Validators.required],
      password: [null, Validators.required],
    });
  }

  get user() { return this.loginFormGroup.get('user'); }

  get password() { return this.loginFormGroup.get('password'); }
}
