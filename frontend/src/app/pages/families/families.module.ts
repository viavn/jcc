import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/shared/modules/material/material.module';
import { FamiliesRoutingModule } from './families.routes';
import { FamiliesComponent } from './families/families.component';
import { ListFamiliesComponent } from './list-families/list-families.component';

@NgModule({
  imports: [
    CommonModule,
    FamiliesRoutingModule,
    MaterialModule,
    ReactiveFormsModule,
  ],
  declarations: [
    FamiliesComponent,
    ListFamiliesComponent,
  ],
})
export class FamilyModule { }
