import { IModels, IDataSource } from './IDataSource';
import { Observable } from 'rxjs';
import { HttpHeaders, HttpClient } from '@angular/common/http';

export abstract class GenericDataSource<T, TK extends IModels<T>> implements IDataSource<T, TK> {

    httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json'
      })
    };

    constructor(private http: HttpClient) { }

    add(item: T, options: any = null): Observable<T> {
      const url = this.getBaseUrl(options) + this.getResource(options);
      const body = JSON.stringify(item);
      return this.http.post<T>(url, body, this.httpOptions);
    }

    edit(item: T, options: any = null): Observable<T> {
      const url = this.getBaseUrl(options) + this.getResource(options);
      const body = JSON.stringify(item);
      return this.http.put<T>(url, body, this.httpOptions);
    }

    get(page: number, count: number, options: any = null): Observable<TK> {
      const offset = (page - 1) * count;
      const url = `${this.getBaseUrl(options)}${this.getResource(options)}?offset=${offset}&count=${count}`;
      return this.http.get<TK>(url, this.httpOptions);
    }

    delete(id: any, options: any = null): Observable<any> {
      // const url = `${this.getBaseUrl(options)}${this.getResource(options)}/${id}`;
      const url = ``;
      return this.http.delete(url, this.httpOptions);
    }

    getById(id: any, options: any = null): Observable<T> {
      const url = `${this.getBaseUrl(options)}${this.getResource(options)}/${id}`;
      return this.http.get<T>(url, this.httpOptions);
    }

    abstract getBaseUrl(options: any): string;
    abstract getResource(options: any): string;
}
