import { TestBed } from '@angular/core/testing';

import { GodParentService } from './god-parent.service';

describe('GodParentService', () => {
  let service: GodParentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GodParentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
