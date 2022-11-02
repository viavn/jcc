import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { catchError, EMPTY, Observable, of, Subject, switchMap, takeUntil, tap } from 'rxjs';
import { SpinnerDialogComponent } from 'src/app/components/spinner-dialog/spinner-dialog.component';
import { TypeResponse } from 'src/app/services/child/models/Child';
import { FamilyService } from 'src/app/services/family/family.service';
import { BaseFamilyRequest, CreateFamilyRequest, FamilyByIdResponse, MemberRequest, MemberViewModel } from 'src/app/services/family/models/FamilyModels';
import { MemberTypeService } from 'src/app/services/member-type/member-type.service';
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

  private memberTypes: TypeResponse[] = [];
  private family!: FamilyByIdResponse;

  familyFormGroup: FormGroup;
  familyMemberFormGroup: FormGroup;
  displayedColumns: string[] = ['type', 'name', 'actions'];
  members: MemberViewModel[] = [];
  memberTypes$!: Observable<TypeResponse[]>;
  familySubmitBtnText = 'Criar';
  memberSubmitBtnText = 'Criar';

  @ViewChild(MatTable) table!: MatTable<MemberViewModel>;

  constructor(
    private familyService: FamilyService,
    private memberTypeService: MemberTypeService,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    public dialog: MatDialog,
    private notificationService: NotificationService,
  ) {
    this.familyFormGroup = this.createFamilyFormGroup();
    this.familyMemberFormGroup = this.createFamilyMemberFormGroup();
  }

  ngOnInit(): void {
    this.memberTypes$ = this.memberTypeService.get().pipe(
      takeUntil(this.destroy$),
      tap((response) => {
        this.memberTypes = [...response]
      }),
      catchError(error => {
        console.error('Erro ao obter tipos dos membros da família', error);
        return of([]);
      }));

    const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
      disableClose: true,
    });

    this.route.paramMap
      .pipe(
        takeUntil(this.destroy$),
        switchMap((params: ParamMap) => {
          const familyId = params.get('id');
          if (!!familyId) {
            return this.familyService.getFamily(familyId);
          }

          spinnerDialogRef.close();
          return EMPTY;
        }),
        catchError(response => {
          console.error('Erro ao obter família', response);
          const message = response.error.data?.length > 0
            ? response.error.data[0]
            : 'Um erro ocorreu ao obter família. Tente novamente!';

          this.notificationService.emitMessage({
            Message: message,
            ShowNotification: true,
            ShowtimeInMilliseconds: 5000,
            Type: NotificationType.ERROR,
          });
          spinnerDialogRef.close();
          return EMPTY;
        }),
      )
      .subscribe(family => {
        this.familySubmitBtnText = 'Atualizar';
        this.family = family;
        this.familyFormGroup = this.createFamilyFormGroup(family);
        this.members = family.members.map((member, index) => {
          return {
            rowId: index + 1,
            personName: member.name,
            member: {
              id: member.id,
              name: member.legalPerson.description,
              type: member.legalPerson.id,
            }
          }
        });
        this.table.renderRows();
        spinnerDialogRef.close();
      });
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
    this.destroySubject.complete();
  }

  backToListPage(): void {
    this.router.navigate(['families']);
  }

  cancel(): void {
    this.familyFormGroup.reset(this.family);
  }

  onSubmitFamily(): void {
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

    const baseRequest: BaseFamilyRequest = {
      code: familyFormValue.code,
      address: familyFormValue.address,
      comment: familyFormValue.comment,
      contactNumber: familyFormValue.contactNumber,
    };

    if (!!this.family) {
      this.familyService.update(this.family.id, baseRequest)
        .pipe(
          takeUntil(this.destroy$),
          catchError(response => {
            console.error('Erro ao atualizar família', response);
            const message = response.error.data?.length > 0
              ? response.error.data[0]
              : 'Um erro ocorreu ao atualizar família. Tente novamente!';

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
            this.family = {
              ...this.family,
              contactNumber: baseRequest.contactNumber,
              address: baseRequest.address,
              code: baseRequest.code,
              comment: baseRequest.comment
            }
            this.familyFormGroup.reset(this.family)
            spinnerDialogRef.close();
          });

    } else {
      const request: CreateFamilyRequest = {
        ...baseRequest,
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
          (response) => {
            this.family = response;
            this.members = this.family.members.map((member, index) => {
              return {
                rowId: index + 1,
                personName: member.name,
                member: {
                  id: member.id,
                  name: member.legalPerson.description,
                  type: member.legalPerson.id,
                }
              }
            });
            this.familyFormGroup.reset(this.family);
            this.table.renderRows();
            this.familySubmitBtnText = 'Atualizar';
            spinnerDialogRef.close();
          });
    }
  }

  onSubmitMember(familyMemberFormElement: FormGroupDirective): void {
    const sortedGifts = this.members.sort((a, b) => a.rowId - b.rowId);
    let newRowId = 1;

    if (sortedGifts.length > 0) {
      newRowId = sortedGifts[sortedGifts.length - 1].rowId + 1;
    }

    const memberFormValue = this.familyMemberFormGroup.value;
    const typeIndex = this.memberTypes.findIndex(g => g.id === this.familyMemberFormGroup.controls['type'].value);

    if (this.family && !memberFormValue.id) {
      const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
        disableClose: true,
      });

      const request: MemberRequest = {
        id: '',
        name: memberFormValue.name,
        type: memberFormValue.type,
      };
      this.familyService.createMember(this.family.id, request)
        .pipe(
          takeUntil(this.destroy$),
          catchError(response => {
            console.error('Erro ao criar membro família', response);
            const message = response.error.data?.length > 0
              ? response.error.data[0]
              : 'Um erro ocorreu ao criar membro família. Tente novamente!';

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
          (response) => {
            this.members.push({
              rowId: newRowId,
              member: {
                id: response.id,
                name: this.memberTypes[typeIndex].description,
                type: memberFormValue.type,
              },
              personName: memberFormValue.name,
            });
            familyMemberFormElement.resetForm({});
            this.table.renderRows();
            spinnerDialogRef.close();
          });
    } else if (this.family && memberFormValue.id) {
      const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
        disableClose: true,
      });

      const request: MemberRequest = {
        id: memberFormValue.id,
        name: memberFormValue.name,
        type: this.familyMemberFormGroup.controls['type'].value,
      };
      this.familyService.updateMember(this.family.id, request)
        .pipe(
          takeUntil(this.destroy$),
          catchError(response => {
            console.error('Erro ao atualizar membro família', response);
            const message = response.error.data?.length > 0
              ? response.error.data[0]
              : 'Um erro ocorreu ao atualizar membro família. Tente novamente!';

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
            const memberIndex = this.members.findIndex(m => m.member.id === request.id);
            this.members[memberIndex] = {
              ...this.members[memberIndex],
              member: {
                id: request.id,
                name: this.memberTypes[typeIndex].description,
                type: request.type,
              },
              personName: memberFormValue.name,
            };

            this.memberSubmitBtnText = 'Criar';
            this.familyMemberFormGroup.get('type')?.enable();
            familyMemberFormElement.resetForm({});
            this.table.renderRows();
            spinnerDialogRef.close();
          });
    } else if (!this.family && memberFormValue.id) {
      const memberIndex = this.members.findIndex(m => m.rowId === parseInt(memberFormValue.id, 10));
      this.members[memberIndex] = {
        ...this.members[memberIndex],
        personName: memberFormValue.name,
      };

      this.memberSubmitBtnText = 'Criar';
      this.familyMemberFormGroup.get('type')?.enable();
      familyMemberFormElement.resetForm({});
      this.table.renderRows();
    } else {
      this.members.push({
        rowId: newRowId,
        member: {
          id: '',
          name: this.memberTypes[typeIndex].description,
          type: memberFormValue.type,
        },
        personName: memberFormValue.name,
      });

      this.table.renderRows();
      familyMemberFormElement.resetForm({});
    }
  }

  cancelMember(memberFormElement: FormGroupDirective): void {
    memberFormElement.resetForm({});
    this.memberSubmitBtnText = 'Criar';
    this.familyMemberFormGroup.get('type')?.enable();
  }

  removeMember(event: any, row: MemberViewModel): void {
    event.stopPropagation();

    if (!!row.member.id) {
      const spinnerDialogRef = this.dialog.open(SpinnerDialogComponent, {
        disableClose: true,
      });

      this.familyService.deleteMember(this.family.id, row.member.id)
        .pipe(
          takeUntil(this.destroy$),
          catchError(response => {
            console.error('Erro ao excluir membro família', response);
            const message = response.error.data?.length > 0
              ? response.error.data[0]
              : 'Um erro ocorreu ao remover membro família. Tente novamente!';

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
            spinnerDialogRef.close();
            this.members = this.members.filter(mem => mem.rowId !== row.rowId);
            this.table.renderRows();
          });
    } else {
      this.members = this.members.filter(mem => mem.rowId !== row.rowId);
      this.table.renderRows();
    }
  }

  onRowMemberClicked(row: MemberViewModel): void {
    this.memberSubmitBtnText = 'Atualizar';
    this.familyMemberFormGroup.patchValue({
      id: !!this.family ? row.member.id : row.rowId.toString(),
      name: row.personName,
      type: row.member.type
    });
    this.familyMemberFormGroup.get('type')?.disable();
  }

  private createFamilyFormGroup(family?: FamilyByIdResponse): FormGroup {
    return this.fb.group({
      id: [family ? family.id : null],
      code: [family ? family.code : null, Validators.required],
      contactNumber: [family ? family.contactNumber : null, Validators.required],
      address: [family ? family.address : null, Validators.required],
      comment: [family ? family.comment : null],
    });
  }

  private createFamilyMemberFormGroup(): FormGroup {
    return this.fb.group({
      id: [''],
      name: [null, Validators.required],
      type: [null, Validators.required],
    });
  }
}
