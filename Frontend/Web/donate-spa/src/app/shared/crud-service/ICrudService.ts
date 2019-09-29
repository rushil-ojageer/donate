import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { Observable } from 'rxjs';
import { DataItems } from '../crud-screen/generic-crud-list/helper-classes/IListComponent';

export interface ICrudService<T> {
    items: Observable<DataItems<T>>;
    load(page: number, count: number, options: any): Observable<any>;
    createOrUpdate(mode: CreateUpdateMode, charity: T, options: any): Observable<any>;
    add(item: T, options: any): Observable<any>;
    edit(item: T, options: any): Observable<any>;
    delete(item: T, options: any): Observable<boolean>;
    getById(id: any, options: any): Observable<T>;
}
