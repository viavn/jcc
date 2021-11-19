import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { catchError, of } from 'rxjs';
import { ChildService } from '../services/child/child.service';
import { Child, DashChildModel } from '../services/child/models/Child';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['familyAcronym', 'name', 'legalResponsible'];
  dataSource = new MatTableDataSource<DashChildModel>([]);
  showLoading = false;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private childService: ChildService,
    private router: Router
  ) {
    // this.dataSource = new MatTableDataSource<DashChildModel>([]);
  }

  ngOnInit(): void {
    this.showLoading = true;
    this.childService.getChildren()
      .subscribe(children => {
        this.dataSource = new MatTableDataSource<DashChildModel>(children);
        this.showLoading = false;
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
}
