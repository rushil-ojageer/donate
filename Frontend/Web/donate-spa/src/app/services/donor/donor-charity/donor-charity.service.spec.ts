import { TestBed } from '@angular/core/testing';

import { DonorCharityService } from './donor-charity.service';

describe('DonorCharityService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DonorCharityService = TestBed.get(DonorCharityService);
    expect(service).toBeTruthy();
  });
});
