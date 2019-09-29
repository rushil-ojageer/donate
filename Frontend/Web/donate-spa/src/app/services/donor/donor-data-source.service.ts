import { Injectable } from '@angular/core';
import { GenericDataSource } from 'src/app/shared/crud-service/GenericDataSource';
import { Donor, Donors } from 'src/app/models/Donor';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DonorDataSourceService extends GenericDataSource<Donor, Donors> {

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  getBaseUrl(options: any): string {
    return 'http://34.90.158.29/donors/api/';
  }

  getResource(options: any): string {
    return 'donor';
  }

}
