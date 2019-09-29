import { Injectable } from '@angular/core';
import { GenericDataSource } from 'src/app/shared/crud-service/GenericDataSource';
import { Donation, Donations } from 'src/app/models/Donation';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DonorDonationDataSourceService extends GenericDataSource<Donation, Donations> {

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  getBaseUrl(options: any): string {
    return 'http://34.90.158.29/donors/api/';
  }

  getResource(options: any): string {
    return `donor/${options.donorId}/donations`;
  }

}
