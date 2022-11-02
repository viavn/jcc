import { Routes, RouterModule } from "@angular/router";
import { NgModule } from '@angular/core';
import { EditAccountComponent } from "./edit/edit-account.component";
import { CreateAccountComponent } from "./create/create-account.component";
import { ManageAccountsComponent } from "./manage-accounts/manage-accounts.component";
import { ListAccountsComponent } from "./list-accounts/list-accounts.component";

const routes: Routes = [
  {
    path: '', component: ManageAccountsComponent,
    children: [
      { path: 'edit/:id', component: EditAccountComponent },
      { path: 'create', component: CreateAccountComponent },
      { path: '', component: ListAccountsComponent },
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
