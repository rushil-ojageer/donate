import { ICrudService } from './ICrudService';
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { CreateUpdateMode } from 'src/app/models/CreateUpdateMode';
import { IDataSource, IModels } from './IDataSource';
import { DataItems } from '../crud-screen/generic-crud-list/helper-classes/IListComponent';

export abstract class GenericCrudService<T, TK extends IModels<T>> implements ICrudService<T> {

    private subject = new BehaviorSubject<DataItems<T>>(new DataItems<T>());
    private pagesSubject = new BehaviorSubject<number>(0);
    private dataStore: { items: DataItems<T> } = { items: new DataItems<T>() };
    private lastPageRequested = 1;
    private lastNumberOfItemsRequested = 10;

    items: Observable<DataItems<T>> = this.subject.asObservable();

    load(page: number, count: number, options: any = null): Observable<any> {
        const subject = new Subject<any>();
        this.getDataSource().get(page, count, options).subscribe(response => {
          const dataItems = new DataItems<T>();
          dataItems.items = response.items;
          dataItems.currentPage = page;
          dataItems.itemsPerPage = count;
          dataItems.totalItems = response.total;
          this.dataStore.items = dataItems;
          this.lastPageRequested = this.dataStore.items.currentPage;
          this.lastNumberOfItemsRequested = this.dataStore.items.itemsPerPage;
          this.subject.next(Object.assign({}, this.dataStore).items);
        }, (error: any) => {
            subject.error(error);
        });
        return subject;
    }

    createOrUpdate(mode: CreateUpdateMode, item: T, options: any = null): Observable<any> {
        if (this.isCreating(mode)) {
            return this.add(item, options);
        }
        if (this.isUpdating(mode)) {
            return this.edit(item, options);
        }
    }

    add(item: T, options: any = null): Observable<any> {
        const subject = new Subject<any>();
        this.getDataSource().add(item, options).subscribe(response => {
            this.load(this.lastPageRequested, this.lastNumberOfItemsRequested, options);
            subject.next(true);
        }, (error: any) => {
            subject.error(error);
        });
        return subject;
    }

    edit(item: T, options: any = null): Observable<any> {
        const subject = new Subject<any>();
        this.getDataSource().edit(item, options).subscribe(response => {
            this.mapFields(response, item);
            this.load(this.lastPageRequested, this.lastNumberOfItemsRequested, options);
            subject.next(true);
        }, (error: any) => {
            subject.error(error);
        });
        return subject;
    }

    delete(item: T, options: any = null): Observable<boolean> {
        const subject = new Subject<boolean>();
        this.getDataSource().delete(this.getId(item), options).subscribe(response => {
            this.load(this.lastPageRequested, this.lastNumberOfItemsRequested, options);
            subject.next(true);
        }, (error: any) => {
            subject.error(error);
        });
        return subject;
    }

    getById(id: any, options: any = null): Observable<T> {
        return this.getDataSource().getById(id, options);
    }

    private isCreating(mode: CreateUpdateMode): boolean {
        return mode === CreateUpdateMode.Create;
    }

    private isUpdating(mode: CreateUpdateMode): boolean {
        return mode === CreateUpdateMode.Update;
    }

    abstract getDataSource(): IDataSource<T, TK>;
    abstract mapFields(response: T, item: T): void;
    abstract getId(item: T): any;
}
