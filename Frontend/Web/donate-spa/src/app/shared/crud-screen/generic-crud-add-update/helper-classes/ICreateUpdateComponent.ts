import { Observable } from 'rxjs';
import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';

export interface ICreateUpdateComponent<T> {
    createOrUpdate(mode: CreateUpdateMode, item: T): Observable<any>;
    getTitle(): string;
}
