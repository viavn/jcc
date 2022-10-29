import { TestBed } from '@angular/core/testing';

import { GiftTypeService } from './gift-type.service';

describe('GiftTypeService', () => {
  let service: GiftTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GiftTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
