import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { catchError, EMPTY, Observable, of, Subject, switchMap, takeUntil } from 'rxjs';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { UserType } from 'src/app/services/auth/enums/UserType';
import { TypeResponse } from 'src/app/services/child/models/Child';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { UserService } from 'src/app/services/user/user.service';
import { UserTypeService } from 'src/app/services/userType/user-type.service';

@Component({
  selector: 'app-edit-account',
  templateUrl: './edit-account.component.html',
  styleUrls: ['./edit-account.component.scss']
})
export class EditAccountComponent implements OnInit, OnDestroy {
  accountFormGroup: FormGroup;
  userTypes$!: Observable<TypeResponse[]>;

  private destroySubject = new Subject<void>();
  private destroy$ = this.destroySubject.asObservable();

  constructor(
    private userService: UserService,
    private userTypeService: UserTypeService,
    private notificationService: NotificationService,
    private route: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog,
    private fb: FormBuilder,
  ) {
    this.accountFormGroup = this.fb.group({
      id: [{ value: null, disabled: true }],
      login: [{ value: null, disabled: true }],
      name: [{ value: null, disabled: true }],
      userType: [{ value: null, disabled: true }],
      isDeleted: [{ value: false, disabled: true }],
    });
  }

  ngOnInit(): void {
    this.userTypes$ = this.userTypeService.get().pipe(
      takeUntil(this.destroy$),
      catchError(error => {
        console.error('Erro ao obter tipos de usuários', error);
        return of([]);
      }));

    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.route.paramMap
      .pipe(
        takeUntil(this.destroy$),
        switchMap((params: ParamMap) => {
          const userId = params.get('id');
          if (!!userId) {
            return this.userService.getUserById(userId);
          }

          spinnerDialogRef.close();
          return EMPTY;
        }),
        catchError(response => {
          console.error('Erro ao obter usuário', response);
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao obter usuário. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
          spinnerDialogRef.close();
          return EMPTY;
        }),
      )
      .subscribe(user => {
        this.accountFormGroup.patchValue({
          id: user.id,
          login: user.login,
          name: user.name,
          userType: user.userType,
          isDeleted: user.isDeleted,
        });
        spinnerDialogRef.close();
      });
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  disableUser(): void {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.userService.disableUser(this.accountFormGroup.get('id')?.value)
      .pipe(
        takeUntil(this.destroy$),
        catchError(error => {
          spinnerDialogRef.close();
          console.error('Erro ao inativar usuário', error)
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao inativar o usuário. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });

          return EMPTY;
        })
      )
      .subscribe(() => {
        this.router.navigateByUrl('/manage-accounts');
        spinnerDialogRef.close();
      });
  }

  cancel(): void {
    this.router.navigateByUrl('/manage-accounts');
  }
}
