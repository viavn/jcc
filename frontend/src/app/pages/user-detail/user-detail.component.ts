import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { AuthService } from 'src/app/services/auth/auth.service';
import { User } from 'src/app/services/auth/models/User';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit {

  loginFormGroup!: FormGroup;
  hidePassword = true;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    public dialog: MatDialog,
    private notificationService: NotificationService,
    private authService: AuthService,
    private userService: UserService,
  ) { }

  ngOnInit(): void {
    const user = this.authService.getUserInSessionStorage();
    if (!user) {
      this.notificationService.emitMessage({
        Message: 'Um erro ocorreu ao obter as informações do usuário. Tente novamente mais tarde!',
        ShowNotification: true,
        ShowtimeInMilliseconds: 5000,
        Type: NotificationType.WARN
      });
      this.router.navigateByUrl('/');

    } else {
      this.loginFormGroup = this.createUserFormGroup(user);
    }
  }

  onSubmit(): void {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    const userFormValues = this.loginFormGroup.value;

    this.userService.changeUserPassword(userFormValues.userId, userFormValues.password)
      .subscribe(() => {
        this.notificationService.emitMessage({
          Message: 'Senha alterada com sucesso!',
          ShowNotification: true,
          ShowtimeInMilliseconds: 5000,
          Type: NotificationType.INFO,
        });
        spinnerDialogRef.close();
        this.router.navigateByUrl('/');
      },
        error => {
          console.error('Erro ao mudar senha', error);
          spinnerDialogRef.close();
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao tentar alterar a senha. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
        });
  }

  private createUserFormGroup(user: User): FormGroup {
    return this.fb.group({
      userId: [user.id],
      login: new FormControl({ value: user.login, disabled: true }, Validators.required),
      password: [null, Validators.required],
    });
  }

  get password() { return this.loginFormGroup.get('password'); }
}
