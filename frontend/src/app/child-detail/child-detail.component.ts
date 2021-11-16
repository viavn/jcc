import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChildService } from '../services/child/child.service';
import { Location } from '@angular/common';
import { Child, GodParent } from '../services/child/models/Child';
import { FormBuilder, Validators } from '@angular/forms';
import { MatTable } from '@angular/material/table';

@Component({
  selector: 'app-child-detail',
  templateUrl: './child-detail.component.html',
  styleUrls: ['./child-detail.component.scss']
})
export class ChildDetailComponent implements OnInit {

  child: Child | undefined;
  godParents: GodParent[] = [];
  displayedColumns: string[] = ['clothes', 'shoes', 'gift', 'name', 'phone', 'actions'];

  formGroup = this.fb.group({
    child: this.fb.group({
      id: ['', Validators.required],
      name: [{ value: '', disabled: true }, Validators.required],
      age: [{ value: '', disabled: true }, Validators.required],
      clothesSize: [{ value: '', disabled: true }, Validators.required],
      shoeSize: [{ value: '', disabled: true }, Validators.required],
    }),

    family: this.fb.group({
      id: [{ value: '', disabled: true }, Validators.required],
      responsible: [{ value: '', disabled: true }, Validators.required],
      phone: [{ value: '', disabled: true }, Validators.required],
      address: [{ value: '', disabled: true }, Validators.required],
    }),

    godParent: this.fb.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
      clothesSelected: [false],
      shoeSelected: [false],
      giftSelected: [false],
    }),
  });

  @ViewChild(MatTable) table!: MatTable<GodParent>;

  constructor(
    private route: ActivatedRoute,
    private childService: ChildService,
    private location: Location,
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.childService.getChild(this.route.snapshot.paramMap.get('id')!)
      .subscribe(child => {
        this.child = child;
        if (child) {
          this.formGroup.patchValue({
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
            }
          });
        }
      });
  }

  onSubmit(): void {
    const godParent = this.formGroup.value.godParent;

    this.godParents.push({
      id: '',
      name: godParent.name,
      phone: godParent.phone,
      isGiftSelected: godParent.giftSelected,
      isClothesSelected: godParent.clothesSelected,
      isShoeSelected: godParent.shoeSelected,
      childId: this.formGroup.value.child.id,
    });

    this.table.renderRows();
    this.formGroup.get('godParent')!.reset();
  }
}
