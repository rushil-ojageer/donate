import { Observable } from 'rxjs';

export interface IDataSource<T, TK extends IModels<T>> {
    add(charity: T, options: any): Observable<T>;
    edit(charity: T, options: any): Observable<T>;
    get(page: number, count: number, options: any): Observable<TK>;
    delete(id: any, options: any): Observable<any>;
    getById(id: any, options: any): Observable<T>;
}

export interface IModels<T> {
    items: T[];
    total: number;
}
