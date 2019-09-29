import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEvent, HttpResponse } from '@angular/common/http';
import { Charity, Charities } from 'src/app/models/Charity';
import { Observable } from 'rxjs';
import { IDataSource } from 'src/app/shared/crud-service/IDataSource';
import { GenericDataSource } from 'src/app/shared/crud-service/GenericDataSource';

@Injectable({
  providedIn: 'root'
})
export class CharityDataSourceService extends GenericDataSource<Charity, Charities> {

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  getBaseUrl(options: any): string {
    return 'http://34.90.158.29/charities/api/';
  }

  getResource(options: any): string {
    return 'charity';
  }

  search(search: string, page: number, count: number, options: any = null): Observable<Charities> {
    const offset = (page - 1) * count;
    const url = `${this.getBaseUrl(options)}${this.getResource(options)}/search?token=${search}&offset=${offset}&count=${count}`;
    return this.httpClient.get<Charities>(url, this.httpOptions);
  }
}
