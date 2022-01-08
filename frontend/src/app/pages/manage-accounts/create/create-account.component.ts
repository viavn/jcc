import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { UserType } from 'src/app/services/auth/enums/UserType';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { CreateUserModel } from 'src/app/services/user/models/CreateUserModel';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.scss']
})
export class CreateAccountComponent {
  accountFormGroup: FormGroup;
  private readonly userTypeDictionary = new Map<number, string>();

  constructor(
    private router: Router,
    public dialog: MatDialog,
    private notificationService: NotificationService,
    private userService: UserService,
    private fb: FormBuilder,
  ) {
    this.accountFormGroup = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required],
      name: ['', Validators.required],
      userType: [null, Validators.required],
    });

    this.userTypeDictionary.set(UserType.ADMIN, 'Administrador');
    this.userTypeDictionary.set(UserType.REGULAR, 'Normal');
  }

  private getUserTypeDescription(userType: UserType): string {
    let description = this.userTypeDictionary.get(userType);
    return !!description ? description : 'Desconhecido';
  }

  onCreateUser(): void {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    const createUserModel: CreateUserModel = {
      ...this.accountFormGroup.value
    };

    this.userService.createUser(createUserModel)
      .subscribe(() => {
        this.router.navigateByUrl('/manage-accounts');
        spinnerDialogRef.close();
      },
        error => {
          spinnerDialogRef.close();
          console.log('Erro ao criar usuário', error)
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao criar o usuário. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
        });
  }
}
