import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChildDetailComponent } from './child-detail/child-detail.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'detail/:id', component: ChildDetailComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
