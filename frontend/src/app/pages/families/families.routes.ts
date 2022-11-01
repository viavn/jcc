import { Routes, RouterModule } from "@angular/router";
import { NgModule } from '@angular/core';
import { ListFamiliesComponent } from "./list-families/list-families.component";
import { FamiliesComponent } from "./families/families.component";
import { CreateFamilyComponent } from "./create/create-family.component";

const routes: Routes = [
  {
    path: '',
    component: FamiliesComponent,
    children: [
      { path: 'create', component: CreateFamilyComponent },
      {
        path: '', component: ListFamiliesComponent,
        // canActivateChild: [AuthGuard],
      }
    ]
  },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class FamiliesRoutingModule { }
