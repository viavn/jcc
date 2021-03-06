import { CommonModule } from '@angular/common';
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/shared/modules/material/material.module';
import { BooleanToStringConverterPipe } from 'src/app/shared/pipes/boolean-to-string-converter.pipe';
import { CreateAccountComponent } from './create/create-account.component';
import { EditAccountComponent } from './edit/edit-account.component';
import { ManageAccountsAppRootComponent } from './manage-accounts-root.component';
import { ManageAccountsComponent } from './manage-accounts.component';
import { ManageAccountsRoutingModule } from './manage-accounts.routes';

@NgModule({
  declarations: [
    BooleanToStringConverterPipe,
    ManageAccountsAppRootComponent,
    ManageAccountsComponent,
    EditAccountComponent,
    CreateAccountComponent,
  ],
  imports: [
    MaterialModule,
    CommonModule,
    ManageAccountsRoutingModule,
    ReactiveFormsModule,
  ],
  providers: [],
  exports: []
})
export class ManageAccountsModule { }
