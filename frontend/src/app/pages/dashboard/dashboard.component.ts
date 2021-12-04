import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { catchError } from 'rxjs';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { ChildService } from '../../services/child/child.service';
import { Child, DashChildModel } from '../../services/child/models/Child';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['familyAcronym', 'name', 'legalResponsible'];
  dataSource = new MatTableDataSource<DashChildModel>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
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
      .subscribe(children => {
        this.dataSource.data = [...children];
        spinnerDialogRef.close();
      },
        error => {
          spinnerDialogRef.close();
          console.log('Erro ao obter crianças', error)
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao obter as crianças. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
        });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onRowClicked(child: Child): void {
    this.router.navigate(['detail', child.id]);
  }

  getReport() {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.childService.getChildrenReport()
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
      },
        error => {
          spinnerDialogRef.close();
          console.log('Erro ao obter relatório', error)
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao obter o relatório. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
        });
  }
}
