import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, EMPTY, Subject, takeUntil } from 'rxjs';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { FamilyService } from 'src/app/services/family/family.service';
import { CreateFamilyRequest, MemberRequest, MemberViewModel } from 'src/app/services/family/models/FamilyModels';
import { NotificationType } from 'src/app/services/notification/models/SystemNotification';
import { NotificationService } from 'src/app/services/notification/notification.service';

@Component({
  selector: 'app-create-family',
  templateUrl: './create-family.component.html',
  styleUrls: ['./create-family.component.scss']
})
export class CreateFamilyComponent implements OnInit, OnDestroy {
  private destroySubject = new Subject<void>();
  private destroy$ = this.destroySubject.asObservable();

  familyFormGroup: FormGroup;
  familyMemberFormGroup: FormGroup;
  displayedColumns: string[] = ['type', 'name', 'actions'];
  members: MemberViewModel[] = [];

  @ViewChild(MatTable) table!: MatTable<MemberViewModel>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private familyService: FamilyService,
    private fb: FormBuilder,
    public dialog: MatDialog,
    private notificationService: NotificationService,
  ) {
    this.familyFormGroup = this.createFamilyFormGroup();
    this.familyMemberFormGroup = this.createFamilyMemberFormGroup();
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  backToListPage(): void {
    this.router.navigate(['/families']);
  }

  cancel(familyFormElement: FormGroupDirective): void {
    familyFormElement.resetForm({});
    // this.resetSubmitButtonText();
  }

  onSubmitFamily(familyFormElement: FormGroupDirective): void {
    if (this.members.length === 0) {
      this.notificationService.emitMessage({
        Message: 'Adicione ao menos um membro.',
        ShowNotification: true,
        ShowtimeInMilliseconds: 5000,
        Type: NotificationType.ERROR,
      });
      return;
    }

    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    const familyFormValue = this.familyFormGroup.value;
    const request: CreateFamilyRequest = {
      code: familyFormValue.code,
      address: familyFormValue.address,
      comment: familyFormValue.comment,
      contactNumber: familyFormValue.contactNumber,
      members: this.members.map(({ member }: MemberViewModel) => ({ ...member })),
    };

    this.familyService.create(request)
      .pipe(
        takeUntil(this.destroy$),
        catchError(response => {
          console.error('Erro ao criar família', response);
          const message = response.error.data?.length > 0
            ? response.error.data[0]
            : 'Um erro ocorreu ao criar família. Tente novamente!';

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
          // this.giftsFormGroup.get('giftType')?.enable();
          // this.table.renderRows();
          // familyFormElement.resetForm({});
          // this.resetSubmitButtonText();
          spinnerDialogRef.close();
          this.backToListPage();
        });
  }

  onSubmitMember(familyMemberFormElement: FormGroupDirective): void {
    const memberFormValue = this.familyMemberFormGroup.value;

    const sortedGifts = this.members.sort((a, b) => a.rowId - b.rowId);
    let newRowId = 1;

    if (sortedGifts.length > 0) {
      newRowId = sortedGifts[sortedGifts.length - 1].rowId + 1;
    }

    this.members.push({
      rowId: newRowId,
      member: {
        id: newRowId.toString(),
        name: memberFormValue.name,
        type: memberFormValue.type,
      },
    });

    this.table.renderRows();
    familyMemberFormElement.resetForm({});
  }

  cancelMember(memberFormElement: FormGroupDirective): void {
    memberFormElement.resetForm({});
  }

  removeMember(event: any, row: MemberViewModel): void {
    event.stopPropagation();

    // const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
    //   disableClose: true,
    // });

    // this.familyService.deleteMember(this.child!.id, row.gift.godParent.id, row.gift.giftType.id)
    //   .pipe(
    //     takeUntil(this.destroy$),
    //     catchError(response => {
    //       console.error('Erro ao excluir madrinha/padrinho', response);
    //       const message = response.error.data?.length > 0
    //         ? response.error.data[0]
    //         : 'Um erro ocorreu ao remover madrinha/padrinho. Tente novamente!';

    //       this.notificationService.emitMessage({
    //         Message: message,
    //         ShowNotification: true,
    //         ShowtimeInMilliseconds: 5000,
    //         Type: NotificationType.ERROR,
    //       });
    //       spinnerDialogRef.close();
    //       return EMPTY;
    //     })
    //   )
    //   .subscribe(
    //     () => {
    this.members = this.members.filter(mem => mem.rowId !== row.rowId);
    this.table.renderRows();
    // spinnerDialogRef.close();
    //     });
  }

  private createFamilyFormGroup(): FormGroup {
    return this.fb.group({
      id: [''],
      code: [{ value: null, disabled: false }, Validators.required],
      contactNumber: [{ value: null, disabled: false }, Validators.required],
      address: [{ value: null, disabled: false }, Validators.required],
      comment: [{ value: null, disabled: false }],
    });
  }

  private createFamilyMemberFormGroup(): FormGroup {
    return this.fb.group({
      id: [''],
      name: [{ value: null, disabled: false }, Validators.required],
      type: [{ value: null, disabled: false }, Validators.required],
    });
  }
}
