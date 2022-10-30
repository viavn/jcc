import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from '@angular/forms';
import { NotFoundComponent } from 'src/app/components/not-found/not-found.component';
import { MaterialModule } from 'src/app/shared/modules/material/material.module';
import { FamilyComponent } from './families.component';

@NgModule({
  declarations: [
    FamilyComponent,
    NotFoundComponent,
  ],
  imports: [
    MaterialModule,
    CommonModule,
    ReactiveFormsModule,
  ],
  providers: [],
  exports: []
})
export class FamilyModule { }
