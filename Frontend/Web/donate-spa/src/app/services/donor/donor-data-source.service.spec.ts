import { TestBed } from '@angular/core/testing';

import { DonorDataSourceService } from './donor-data-source.service';

describe('DonorDataSourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DonorDataSourceService = TestBed.get(DonorDataSourceService);
    expect(service).toBeTruthy();
  });
});
