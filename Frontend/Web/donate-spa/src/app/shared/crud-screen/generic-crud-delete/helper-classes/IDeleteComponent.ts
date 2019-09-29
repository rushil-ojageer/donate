import { Observable } from 'rxjs';

export interface IDeleteComponent<T> {
    delete(item: T): Observable<any>;
}
