import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
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
export class LoginComponent {
  loginFormGroup: FormGroup;
  hidePassword = true;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    public dialog: MatDialog,
    private notificationService: NotificationService,
    private authService: AuthService,

  ) {
    this.loginFormGroup = this.createLoginFormGroup();
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
      .subscribe(
        (userInfo: any) => {
          const user: User = {
            id: userInfo.id,
            login: userInfo.login,
            userType: userInfo.userType,
            password: '',
          };
          this.authService.saveUserToSessionStorage(user);
          spinnerDialogRef.close();
          this.authService.emitUserHasLoggedIn();
          this.router.navigate(['/']);
        },
        error => {
          console.error('Erro ao autenticar', error);
          spinnerDialogRef.close();
          this.notificationService.emitMessage({
            Message: 'Usuário ou senhas inválidos tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
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
