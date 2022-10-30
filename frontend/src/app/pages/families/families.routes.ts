import { Routes, RouterModule } from "@angular/router";
import { NgModule } from '@angular/core';
import { FamilyComponent } from "./families.component";
import { NotFoundComponent } from "src/app/components/not-found/not-found.component";

const routes: Routes = [
  {
    path: '', component: FamilyComponent,
    // children: [
    //   { path: 'edit/:id', component: EditAccountComponent },
    //   { path: 'create', component: CreateAccountComponent },
    //   { path: '', component: ManageAccountsComponent },
    // ]
  },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class FamilyRoutingModule { }
