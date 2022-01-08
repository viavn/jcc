import { Routes, RouterModule } from "@angular/router";
import { NgModule } from '@angular/core';
import { ManageAccountsComponent } from "./manage-accounts.component";
import { EditAccountComponent } from "./edit/edit-account.component";
import { ManageAccountsAppRootComponent } from "./manage-accounts-root.component";
import { CreateAccountComponent } from "./create/create-account.component";

const routes: Routes = [
  {
    path: '', component: ManageAccountsAppRootComponent,
    children: [
      { path: 'edit/:id', component: EditAccountComponent },
      { path: 'create', component: CreateAccountComponent },
      { path: '', component: ManageAccountsComponent },
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class ManageAccountsRoutingModule { }
