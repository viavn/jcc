import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './shared/modules/material/material.module';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { SnackbarComponent } from './components/snackbar/snackbar.component';
import { ChildDetailComponent } from './pages/child-detail/child-detail.component';
import { SpinnerDialogComponent } from './components/spinner-dialog/spinner-dialog.component';
import { LoginComponent } from './pages/login/login.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { UserDetailComponent } from './pages/user-detail/user-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ChildDetailComponent,
    SnackbarComponent,
    SpinnerDialogComponent,
    LoginComponent,
    NotFoundComponent,
    UserDetailComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
