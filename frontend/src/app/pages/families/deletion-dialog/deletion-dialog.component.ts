import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogData } from './models/DialogData';

@Component({
  selector: 'app-deletion-dialog',
  templateUrl: './deletion-dialog.component.html',
  styleUrls: ['./deletion-dialog.component.scss']
})
export class DeletionDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData) { }
}
