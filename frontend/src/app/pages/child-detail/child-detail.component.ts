import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { MatTable } from '@angular/material/table';
import { GiftViewModel, GiftViewModel as GodParentViewModel } from './viewModelds/GiftViewModel';
import { CreateGiftRequest, GetChildrenByIdResponse, GodParentResponse, TypeResponse } from 'src/app/services/child/models/Child';
import { ChildService } from 'src/app/services/child/child.service';
import { MatDialog } from '@angular/material/dialog';
import { NotificationService } from 'src/app/services/notification/notification.service';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { catchError, EMPTY, Observable, of, Subject, takeUntil, tap } from 'rxjs';
import { GiftTypeService } from 'src/app/services/gift-type/gift-type.service';
import { GodParentService } from 'src/app/services/godParent/god-parent.service';

@Component({
  selector: 'app-child-detail',
  templateUrl: './child-detail.component.html',
  styleUrls: ['./child-detail.component.scss']
})
export class ChildDetailComponent implements OnInit, OnDestroy {

  child?: GetChildrenByIdResponse;
  gifts: GiftViewModel[] = [];
  displayedColumns: string[] = ['isDelivered', 'giftType', 'name', 'contactNumber', 'address', 'actions'];
  childFormGroup: FormGroup;
  giftsFormGroup: FormGroup;

  private giftTypes: TypeResponse[] = [];
  private readonly addText = 'Adicionar';
  private readonly updateText = 'Atualizar';
  godParentsSubmitButtonTitle = this.addText;

  @ViewChild(MatTable) table!: MatTable<GiftViewModel>;

  private destroySubject = new Subject<void>();
  private destroy$ = this.destroySubject.asObservable();

  giftTypes$!: Observable<TypeResponse[]>;

  constructor(
    private route: ActivatedRoute,
    private childService: ChildService,
    private fb: FormBuilder,
    public dialog: MatDialog,
    private notificationService: NotificationService,
    private giftTypeService: GiftTypeService,
    private godParentService: GodParentService,
    private router: Router,
  ) {
    this.childFormGroup = this.createChildFormGroup();
    this.giftsFormGroup = this.createGiftFormGroup();
  }

  ngOnInit(): void {
    this.giftTypes$ = this.giftTypeService.getTypes().pipe(
      takeUntil(this.destroy$),
      tap((response) => {
        this.giftTypes = [...response]
      }),
      catchError(error => {
        console.error('Erro ao obter tipos dos presentes', error);
        return of([]);
      }));

    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.childService.getChild(this.route.snapshot.paramMap.get('id')!)
      .pipe(
        takeUntil(this.destroy$),
        catchError(error => {
          console.error('Erro ao obter crianças', error);
          this.notificationService.emitMessage({
            Message: 'Um erro ocorreu ao obter os dados da criança. Tente novamente!',
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });

          spinnerDialogRef.close();

          return EMPTY;
        })
      )
      .subscribe(child => {
        this.child = { ...child };
        this.setValuesToChildForm(child);

        this.gifts = child.gifts.map((gift, index) => {
          const viewModel: GiftViewModel = {
            rowId: index + 1,
            gift: { ...gift },
          };
          return viewModel;
        });

        spinnerDialogRef.close();
      });
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  onSubmit(giftFormElement: FormGroupDirective): void {
    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    const giftFormValue = this.giftsFormGroup.value;

    if (giftFormValue.rowId) {
      const request: GodParentResponse = {
        id: giftFormValue.godParentId,
        name: giftFormValue.name,
        contactNumber: giftFormValue.phone,
        address: giftFormValue.address,
      };

      this.godParentService.update(request).pipe(
        takeUntil(this.destroy$),
        catchError(response => {
          console.error('Erro ao atualizar madrinha/padrinho', response);
          const message = response.error.data?.length > 0
            ? response.error.data[0]
            : 'Um erro ocorreu ao atualizar madrinha/padrinho. Tente novamente!';

          this.notificationService.emitMessage({
            Message: message,
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
          spinnerDialogRef.close();
          return EMPTY;
        })
      )
        .subscribe(
          () => {
            const giftIndex = this.gifts.findIndex(gp => gp.rowId === giftFormValue.rowId);
            this.gifts[giftIndex] = {
              rowId: giftFormValue.rowId,
              gift: {
                ...this.gifts[giftIndex].gift,
                godParent: {
                  ...request
                },
              }
            };

            this.giftsFormGroup.get('giftType')?.enable();
            this.table.renderRows();
            giftFormElement.resetForm({});
            this.resetSubmitButtonText();
            spinnerDialogRef.close();
          });
    } else {
      // Criando presente
      const giftCreated = this.gifts.findIndex(row => row.gift.giftType.id === giftFormValue.giftType);
      if (giftCreated > 0) {
        spinnerDialogRef.close();
        this.notificationService.emitMessage({
          Message: 'Presente já foi apadrinhado. Escolha outro tipo para apadrinhar!',
          ShowNotification: true,
          ShowtimeInMilliseconds: 5000,
          Type: NotificationType.ERROR,
        });
        return;
      }

      const sortedGifts = this.gifts.sort((a, b) => a.rowId - b.rowId);
      let newRowId = 1;

      if (sortedGifts.length > 0) {
        newRowId = sortedGifts[sortedGifts.length - 1].rowId + 1;
      }

      const request: CreateGiftRequest = {
        childId: this.child!.id,
        typeId: giftFormValue.giftType,
        godParent: {
          address: giftFormValue.address,
          name: giftFormValue.name,
          contactNumber: giftFormValue.phone,
        }
      };

      this.childService.addGift(request)
        .pipe(
          takeUntil(this.destroy$),
          catchError(response => {
            console.error('Erro ao apadrinhar criança', response);
            const message = response.error.data?.length > 0
              ? response.error.data[0]
              : 'Um erro ocorreu ao remover madrinha/padrinho. Tente novamente!';

            this.notificationService.emitMessage({
              Message: message,
              ShowNotification: true,
              ShowtimeInMilliseconds: 5000,
              Type: NotificationType.ERROR,
            });
            spinnerDialogRef.close();
            return EMPTY;
          })
        )
        .subscribe(
          (respose) => {
            const typeIndex = this.giftTypes.findIndex(g => g.id === respose.type);

            this.gifts.push({
              rowId: newRowId,
              gift: {
                giftType: { ...this.giftTypes[typeIndex] },
                godParent: {
                  id: respose.godParentId,
                  name: respose.godParent.name,
                  address: respose.godParent.address,
                  contactNumber: respose.godParent.contactNumber,
                },
                isDelivered: false
              }
            });

            this.giftsFormGroup.get('giftType')?.enable();
            this.table.renderRows();
            giftFormElement.resetForm({});
            this.resetSubmitButtonText();
            spinnerDialogRef.close();
          });
    }
  }

  cancelGodParentAction(giftFormElement: FormGroupDirective) {
    this.giftsFormGroup.get('giftType')?.enable();
    giftFormElement.resetForm();
    this.resetSubmitButtonText();
  }

  deleteGift(event: any, row: GiftViewModel): void {
    event.stopPropagation();

    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.childService.removeGift(this.child!.id, row.gift.godParent.id, row.gift.giftType.id)
      .pipe(
        takeUntil(this.destroy$),
        catchError(response => {
          console.error('Erro ao excluir madrinha/padrinho', response);
          const message = response.error.data?.length > 0
            ? response.error.data[0]
            : 'Um erro ocorreu ao remover madrinha/padrinho. Tente novamente!';

          this.notificationService.emitMessage({
            Message: message,
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
          spinnerDialogRef.close();
          return EMPTY;
        })
      )
      .subscribe(
        () => {
          this.gifts = this.gifts.filter(gp => gp.rowId !== row.rowId);
          this.table.renderRows();
          spinnerDialogRef.close();
        });
  }

  markGiftAsDelivered(event: any, row: GiftViewModel): void {
    event.stopPropagation();

    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.childService.deliverGift(this.child!.id, row.gift.godParent.id, row.gift.giftType.id)
      .pipe(
        takeUntil(this.destroy$),
        catchError(response => {
          console.error('Erro ao entregar presente', response);
          const message = response.error.data?.length > 0
            ? response.error.data[0]
            : 'Um erro ocorreu ao entregar o presente. Tente novamente!';

          this.notificationService.emitMessage({
            Message: message,
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
          spinnerDialogRef.close();
          return EMPTY;
        })
      )
      .subscribe(
        () => {
          const giftIndex = this.gifts.findIndex(g => g.rowId === row.rowId);
          this.gifts[giftIndex] = {
            ...this.gifts[giftIndex],
            gift: {
              ...this.gifts[giftIndex].gift,
              isDelivered: true
            }
          }
          this.table.renderRows();
          spinnerDialogRef.close();
        });
  }

  onRowClicked(row: GodParentViewModel): void {
    this.godParentsSubmitButtonTitle = this.updateText;
    this.giftsFormGroup.patchValue({
      rowId: row.rowId,
      godParentId: row.gift.godParent.id,
      name: row.gift.godParent.name,
      phone: row.gift.godParent.contactNumber,
      address: row.gift.godParent.address,
      giftType: row.gift.giftType.id,
    });
    this.giftsFormGroup.get('giftType')?.disable();
  }

  backToDashboard(): void {
    this.router.navigate(['/manage-accounts']);
  }

  private createChildFormGroup(): FormGroup {
    return this.fb.group({
      child: this.fb.group({
        id: [null, Validators.required],
        name: [{ value: null, disabled: true }, Validators.required],
        age: [{ value: null, disabled: true }, Validators.required],
        clotheSize: [{ value: null, disabled: true }, Validators.required],
        shoeSize: [{ value: null, disabled: true }, Validators.required],
      }),
      family: this.fb.group({
        id: [{ value: null, disabled: true }, Validators.required],
        code: [{ value: null, disabled: true }, Validators.required],
        member: [{ value: null, disabled: true }, Validators.required],
        contactNumber: [{ value: null, disabled: true }, Validators.required],
        address: [{ value: null, disabled: true }, Validators.required],
      }),
    });
  }

  private resetSubmitButtonText(): void {
    this.godParentsSubmitButtonTitle = this.addText;
  }

  private createGiftFormGroup(): FormGroup {
    return this.fb.group({
      rowId: [null],
      godParentId: [null],
      name: [null, Validators.required],
      phone: [null, Validators.required],
      address: [null, Validators.required],
      giftType: ['', Validators.required],
    });
  }

  private setValuesToChildForm(child: GetChildrenByIdResponse): void {
    this.childFormGroup.patchValue({
      child: {
        id: child.id,
        name: child.name,
        age: child.age,
        clotheSize: child.clotheSize,
        shoeSize: child.shoeSize,
      },
      family: {
        id: child.family.id,
        code: child.family.code,
        member: child.family.member,
        contactNumber: child.family.contactNumber,
        address: child.family.address,
      },
    });
  }
}
