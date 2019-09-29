import { Injectable } from '@angular/core';
import { GenericCrudService } from 'src/app/shared/crud-service/GenericCrudService';
import { Donation, Donations } from 'src/app/models/Donation';
import { IDataSource } from 'src/app/shared/crud-service/IDataSource';
import { DonorDonationDataSourceService } from './donor-donation-data-source.service';

@Injectable({
  providedIn: 'root'
})
export class DonorDonationService  extends GenericCrudService<Donation, Donations> {

  constructor(private donorDonationDataSourceService: DonorDonationDataSourceService) {
    super();
  }

  getDataSource(): IDataSource<Donation, Donations> {
    return this.donorDonationDataSourceService;
  }

  mapFields(response: Donation, item: Donation): void {
  }

  getId(item: Donation): any {
    return item.id;
  }

}
