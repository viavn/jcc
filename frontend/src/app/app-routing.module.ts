import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AdminAuthGuard } from './guards/admin/admin.guard';
import { AuthGuard } from './guards/auth/auth.guard';
import { LoggedInGuard } from './guards/logged-in/loggedIn.guard';
import { ChildDetailComponent } from './pages/child-detail/child-detail.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { LoginComponent } from './pages/login/login.component';
import { UserDetailComponent } from './pages/user-detail/user-detail.component';

const routes: Routes = [
  { path: 'home', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent, canActivate: [LoggedInGuard] },
  { path: 'user', component: UserDetailComponent, canActivate: [AuthGuard] },
  {
    path: 'manage-accounts',
    loadChildren: () => import('./pages/manage-accounts/manage-accounts.module')
      .then(m => m.ManageAccountsModule),
    canActivate: [AdminAuthGuard]
  },
  {
    path: 'families',
    loadChildren: () => import('./pages/families/families.module').then(m => m.FamilyModule),
    canActivate: [AdminAuthGuard]
  },
  { path: 'detail/:id', component: ChildDetailComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
