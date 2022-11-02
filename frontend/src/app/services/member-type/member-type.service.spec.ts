import { TestBed } from '@angular/core/testing';

import { MemberTypeService } from './member-type.service';

describe('MemberTypeService', () => {
  let service: MemberTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MemberTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
