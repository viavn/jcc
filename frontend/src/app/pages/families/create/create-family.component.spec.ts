import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateFamilyComponent } from './create-family.component';

describe('CreateComponent', () => {
  let component: CreateFamilyComponent;
  let fixture: ComponentFixture<CreateFamilyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateFamilyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateFamilyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
