import { TestBed } from '@angular/core/testing';

import { DonorDonationService } from './donor-donation.service';

describe('DonorDonationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DonorDonationService = TestBed.get(DonorDonationService);
    expect(service).toBeTruthy();
  });
});
