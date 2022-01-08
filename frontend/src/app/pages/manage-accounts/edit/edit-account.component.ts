import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { UserType } from 'src/app/services/auth/enums/UserType';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-edit-account',
  templateUrl: './edit-account.component.html',
  styleUrls: ['./edit-account.component.scss']
})
export class EditAccountComponent implements OnInit {
  accountFormGroup: FormGroup;
  private readonly userTypeDictionary = new Map<number, string>();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog,
    private notificationService: NotificationService,
    private userService: UserService,
    private fb: FormBuilder,
  ) {
    this.accountFormGroup = this.fb.group({
      id: [{ value: '', disabled: true }],
      login: [{ value: '', disabled: true }],
      name: [{ value: '', disabled: true }],
      userType: [{ value: '', disabled: true }],
      isDeleted: [{ value: false, disabled: true }],
    });

    this.userTypeDictionary.set(UserType.ADMIN, 'Administrador');
    this.userTypeDictionary.set(UserType.REGULAR, 'Normal');
  }

  ngOnInit(): void {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.userService.getUserById(this.route.snapshot.paramMap.get('id')!)
      .subscribe(user => {
        this.accountFormGroup.patchValue({
          id: user.id,
          login: user.login,
          name: user.name,
          userType: this.getUserTypeDescription(user.userType),
          isDeleted: user.isDeleted,
        });
        spinnerDialogRef.close();
      },
        error => {
          spinnerDialogRef.close();
          console.log('Erro ao obter usuário', error)
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao obter o usuário. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
        });
  }

  private getUserTypeDescription(userType: UserType): string {
    let description = this.userTypeDictionary.get(userType);
    return !!description ? description : 'Desconhecido';
  }

  disableUser(): void {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.userService.disableUser(this.accountFormGroup.get('id')?.value)
      .subscribe(() => {
        this.router.navigateByUrl('/manage-accounts');
        spinnerDialogRef.close();
      },
        error => {
          spinnerDialogRef.close();
          console.log('Erro ao inativar usuário', error)
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao inativar o usuário. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
        });
  }
}
