import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { UserType } from 'src/app/services/auth/enums/UserType';
import { User } from 'src/app/services/auth/models/User';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { GetUsersModel } from 'src/app/services/user/models/GetUsersModel';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-manage-accounts',
  templateUrl: './manage-accounts.component.html',
  styleUrls: ['./manage-accounts.component.scss']
})
export class ManageAccountsComponent implements OnInit, AfterViewInit, OnDestroy {

  displayedColumns: string[] = ['login', 'name', 'isAdmin', 'isDeleted'];
  dataSource = new MatTableDataSource<GetUsersModel>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  private destroySubject = new Subject<void>();
  private destroy$ = this.destroySubject.asObservable();

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private notificationService: NotificationService,
    private userService: UserService,
  ) {
  }

  ngOnInit(): void {
    const spinnerDialog = this.openSpinnerDialog();

    this.userService.getUsers()
      .pipe(
        takeUntil(this.destroy$),
        catchError(error => {
          spinnerDialog.close();
          console.log('Erro ao obter usuários', error)
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao obter usuários. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });

          return EMPTY;
        })
      )
      .subscribe(
        users => {
          this.dataSource.data = [...users];
          spinnerDialog.close();
        });
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  openSpinnerDialog(): MatDialogRef<SpinnerDialogComponent, any> {
    return this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onRowClicked(user: GetUsersModel): void {
    this.router.navigate(['edit', user.id], { relativeTo: this.route });
  }

  isUserAdmin(userType: UserType): boolean {
    return userType === UserType.ADMIN;
  }

  createUser(): void {
    this.router.navigate(['create'], { relativeTo: this.route });
  }
}
