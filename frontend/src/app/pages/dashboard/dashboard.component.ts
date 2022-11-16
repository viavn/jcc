import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { catchError, EMPTY, of, Subject, takeUntil } from 'rxjs';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { ChildService } from '../../services/child/child.service';
import { DashChildModel } from '../../services/child/models/Child';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, AfterViewInit, OnDestroy {
  displayedColumns: string[] = ['familyCode', 'name', 'deliveredGifts', 'remainingGifts'];
  dataSource = new MatTableDataSource<DashChildModel>([]);

  private destroySubject = new Subject<void>();
  private destroy$ = this.destroySubject.asObservable();

  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private childService: ChildService,
    private router: Router,
    public dialog: MatDialog,
    private notificationService: NotificationService
  ) {
  }

  ngOnInit(): void {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.childService.getChildren()
      .pipe(
        takeUntil(this.destroy$),
        catchError((error: any) => {
          console.error('Erro ao obter crianças', error)
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao obter as crianças. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
          return of([]);
        }),
      )
      .subscribe(children => {
        this.dataSource.data = [...children];
        spinnerDialogRef.close();
      });
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  applyFilter(event: any) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onRowClicked({ id }: DashChildModel): void {
    this.router.navigate(['detail', id]);
  }

  getReport() {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.childService.getChildrenReport()
      .pipe(
        takeUntil(this.destroy$),
        catchError(error => {
          spinnerDialogRef.close();
          console.error('Erro ao obter relatório', error)
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao obter o relatório. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });

          return EMPTY;
        })
      )
      .subscribe(data => {
        spinnerDialogRef.close();
        const blob = new Blob([data], { type: 'application/octet-stream' });

        let link = document.createElement("a");
        if (link.download !== undefined) {
          const url = URL.createObjectURL(blob);
          link.setAttribute("href", url);
          link.setAttribute("download", 'crianças.xlsx');
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
        }
      });
  }
}
