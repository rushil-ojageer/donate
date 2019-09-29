import { DonorCharity, DonorCharities } from 'src/app/models/DonorCharity';
import { Injectable } from '@angular/core';
import { GenericCrudService } from 'src/app/shared/crud-service/GenericCrudService';
import { IDataSource } from 'src/app/shared/crud-service/IDataSource';
import { DonorCharityDataSourceService } from './donor-charity-data-source.service';

@Injectable({
  providedIn: 'root'
})
export class DonorCharityService extends GenericCrudService<DonorCharity, DonorCharities> {

  constructor(private donorCharityDataSourceService: DonorCharityDataSourceService) {
    super();
  }

  getDataSource(): IDataSource<DonorCharity, DonorCharities> {
    return this.donorCharityDataSourceService;
  }

  mapFields(response: DonorCharity, item: DonorCharity): void {
  }

  getId(item: DonorCharity): any {
    return item.charityIdentifier;
  }

}
