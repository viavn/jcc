import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChildService } from '../services/child/child.service';
import { Child, GodParent } from '../services/child/models/Child';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTable } from '@angular/material/table';
import { GodParentsViewModel as GodParentViewModel } from './viewModelds/GodParentsViewModel';

@Component({
  selector: 'app-child-detail',
  templateUrl: './child-detail.component.html',
  styleUrls: ['./child-detail.component.scss']
})
export class ChildDetailComponent implements OnInit {

  child: Child | undefined;
  godParents: GodParentViewModel[] = [];
  displayedColumns: string[] = ['clothes', 'shoes', 'gift', 'name', 'phone', 'actions'];
  childFormGroup: FormGroup;
  godParentsFormGroup: FormGroup;

  private readonly addText = 'Adicionar';
  private readonly updateText = 'Atualizar';
  godParentsSubmitButtonTitle = this.addText;

  @ViewChild(MatTable) table!: MatTable<GodParent>;

  constructor(
    private route: ActivatedRoute,
    private childService: ChildService,
    private fb: FormBuilder,
  ) {
    this.childFormGroup = this.createChildFormGroup();
    this.godParentsFormGroup = this.createGodParentsFormGroup();
  }

  ngOnInit(): void {
    this.childService.getChild(this.route.snapshot.paramMap.get('id')!)
      .subscribe(child => {
        this.child = child;
        if (child) {
          this.setValuesToChildForm(child);
        }
      });
  }

  onSubmit(event: any): void {
    const godParentFormValues = this.godParentsFormGroup.value;
    if (godParentFormValues.rowId) {
      const godParentIndex = this.godParents.findIndex(gp => gp.rowId === godParentFormValues.rowId)
      if (godParentIndex >= 0) {
        this.godParents[godParentIndex] = {
          ...this.godParents[godParentIndex],
          godParent: {
            ...this.godParents[godParentIndex].godParent,
            name: godParentFormValues.name,
            phone: godParentFormValues.phone,
            isGiftSelected: godParentFormValues.giftSelected,
            isClothesSelected: godParentFormValues.clothesSelected,
            isShoeSelected: godParentFormValues.shoeSelected,
          }
        };
      }

    } else {
      const sortedGodParents = this.godParents.sort((a, b) => a.createdDate.getTime() - b.createdDate.getTime());
      let lastId = 0;

      if (sortedGodParents.length > 0) {
        lastId = sortedGodParents[sortedGodParents.length - 1].rowId
      }

      this.godParents.push({
        rowId: lastId + 1,
        createdDate: new Date(),
        godParent: {
          id: '',
          name: godParentFormValues.name,
          phone: godParentFormValues.phone,
          isGiftSelected: godParentFormValues.giftSelected,
          isClothesSelected: godParentFormValues.clothesSelected,
          isShoeSelected: godParentFormValues.shoeSelected,
          childId: this.childFormGroup.value.child.id,
        }
      });
    }

    this.table.renderRows();
    event.currentTarget.reset();
    this.godParentsFormGroup.reset();
    this.resetSubmitButtonText();
  }

  cancelGodParentAction() {
    this.godParentsFormGroup.reset();
    this.resetSubmitButtonText();
  }

  deleteGodParentFromTable(godParent: GodParentViewModel): void {
    this.godParents = this.godParents.filter(gp => gp.rowId !== godParent.rowId);
    this.table.renderRows();
  }

  onRowClicked(row: GodParentViewModel): void {
    this.godParentsSubmitButtonTitle = this.updateText;

    this.godParentsFormGroup.patchValue({
      rowId: row.rowId,
      name: row.godParent.name,
      phone: row.godParent.phone,
      clothesSelected: row.godParent.isClothesSelected,
      shoeSelected: row.godParent.isShoeSelected,
      giftSelected: row.godParent.isGiftSelected,
    });
  }

  private createChildFormGroup(): FormGroup {
    return this.fb.group({
      child: this.fb.group({
        id: [null, Validators.required],
        name: [{ value: null, disabled: true }, Validators.required],
        age: [{ value: null, disabled: true }, Validators.required],
        clothesSize: [{ value: null, disabled: true }, Validators.required],
        shoeSize: [{ value: null, disabled: true }, Validators.required],
      }),

      family: this.fb.group({
        id: [{ value: null, disabled: true }, Validators.required],
        responsible: [{ value: null, disabled: true }, Validators.required],
        phone: [{ value: null, disabled: true }, Validators.required],
        address: [{ value: null, disabled: true }, Validators.required],
      }),
    });
  }

  private resetSubmitButtonText(): void {
    this.godParentsSubmitButtonTitle = this.addText;
  }

  private createGodParentsFormGroup(): FormGroup {
    return this.fb.group({
      rowId: [null],
      name: [null, Validators.required],
      phone: [null, Validators.required],
      clothesSelected: [false],
      shoeSelected: [false],
      giftSelected: [false],
    });
  }

  private setValuesToChildForm(child: Child): void {
    this.childFormGroup.patchValue({
      child: {
        id: child.id,
        name: child.name,
        age: child.age,
        clothesSize: child.clothesSize,
        shoeSize: child.shoeSize,
      },
      family: {
        id: child.familyId,
        responsible: child.responsible,
        phone: child.phone,
        address: child.address,
      },
    });
  }
}
