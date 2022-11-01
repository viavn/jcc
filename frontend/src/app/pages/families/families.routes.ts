import { Routes, RouterModule } from "@angular/router";
import { NgModule } from '@angular/core';
import { ListFamiliesComponent } from "./list-families/list-families.component";
import { FamiliesComponent } from "./families/families.component";

const routes: Routes = [
  {
    path: '',
    component: FamiliesComponent,
    children: [
      {
        path: '',
        // canActivateChild: [AuthGuard],
        children: [
          { path: '', component: ListFamiliesComponent }
        ]
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
