import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, EMPTY, of, Subject, takeUntil } from 'rxjs';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { FamilyService } from 'src/app/services/family/family.service';
import { FamilyResponse } from 'src/app/services/family/models/FamilyModels';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { DeletionDialogComponent } from '../deletion-dialog/deletion-dialog.component';

@Component({
  selector: 'app-list-families',
  templateUrl: './list-families.component.html',
  styleUrls: ['./list-families.component.scss']
})
export class ListFamiliesComponent implements OnInit, AfterViewInit, OnDestroy {
  private destroySubject = new Subject<void>();
  private destroy$ = this.destroySubject.asObservable();

  dataSource = new MatTableDataSource<FamilyResponse>([]);
  displayedColumns: string[] = ['code', 'memberName', 'address', 'actions'];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private familyService: FamilyService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private notificationService: NotificationService,
  ) { }

  ngOnInit(): void {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.familyService.getFamilies()
      .pipe(
        takeUntil(this.destroy$),
        catchError((error: any) => {
          console.log('Erro ao obter famílias', error)
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao obter as famílias. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
          return of([]);
        }),
      )
      .subscribe(families => {
        this.dataSource.data = [...families];
        spinnerDialogRef.close();
      });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  onRowClicked({ id }: FamilyResponse): void {
    this.router.navigate(['create', { id }], { relativeTo: this.route });
  }

  applyFilter(event: any) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  create(): void {
    this.router.navigate(['create'], { relativeTo: this.route });
  }

  removeFamily(event: any, { id }: FamilyResponse): void {
    event.stopPropagation();

    this.dialog.open(DeletionDialogComponent, {
      disableClose: true,
    }).afterClosed().subscribe((result: boolean) => {
      console.log(result);
      if (result) {
        this.delete(id);
      }
    });
  }

  private delete(id: string) {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.familyService.delete(id)
      .pipe(
        takeUntil(this.destroy$),
        catchError(response => {
          console.error('Erro ao excluir família', response);
          const message = response.error.data?.length > 0
            ? response.error.data[0]
            : 'Um erro ocorreu ao remover família. Tente novamente!';

          this.notificationService.emitMessage({
            Message: message,
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
          spinnerDialogRef.close();
          return EMPTY;
        })
      )
      .subscribe(
        () => {
          this.dataSource.data = this.dataSource.data.filter(f => f.id !== id);
          // this.table.renderRows();
          spinnerDialogRef.close();
        });
  }
}
