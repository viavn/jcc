import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { catchError, EMPTY, Observable, of, Subject, takeUntil, tap } from 'rxjs';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { TypeResponse } from 'src/app/services/child/models/Child';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { CreateUserModel } from 'src/app/services/user/models/CreateUserModel';
import { UserService } from 'src/app/services/user/user.service';
import { UserTypeService } from 'src/app/services/userType/user-type.service';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.scss']
})
export class CreateAccountComponent implements OnInit, OnDestroy {
  accountFormGroup: FormGroup;
  userTypes$!: Observable<TypeResponse[]>;

  private destroySubject = new Subject<void>();
  private destroy$ = this.destroySubject.asObservable();

  constructor(
    private userService: UserService,
    private userTypeService: UserTypeService,
    private notificationService: NotificationService,
    private router: Router,
    public dialog: MatDialog,
    private fb: FormBuilder,
  ) {
    this.accountFormGroup = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required],
      name: ['', Validators.required],
      userType: [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.userTypes$ = this.userTypeService.get().pipe(
      takeUntil(this.destroy$),
      catchError(error => {
        console.error('Erro ao obter tipos de usuários', error);
        return of([]);
      }));
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  onCreateUser(): void {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    const createUserModel: CreateUserModel = {
      ...this.accountFormGroup.value
    };

    this.userService.createUser(createUserModel)
      .pipe(
        takeUntil(this.destroy$),
        catchError(response => {
          console.error('Erro ao criar usuário', response);
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao criar usuário. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
          spinnerDialogRef.close();
          return EMPTY;
        })
      )
      .subscribe(() => {
        this.router.navigateByUrl('/manage-accounts');
        spinnerDialogRef.close();
      });
  }

  cancel(): void {
    this.accountFormGroup.reset({});
  }

  backToListUser(): void {
    this.router.navigateByUrl('/manage-accounts');
  }
}
