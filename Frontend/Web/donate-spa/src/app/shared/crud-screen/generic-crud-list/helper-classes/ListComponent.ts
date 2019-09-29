import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Observable, BehaviorSubject } from 'rxjs';
import { Row } from 'src/app/models/DataTable';
import { TableAction } from '../generic-crud-list.component';
import { ICrudService } from '../../../crud-service/ICrudService';
import { IListComponent, DataItems } from './IListComponent';
import { IDeleteComponent } from '../../generic-crud-delete/helper-classes/IDeleteComponent';
import { GenericCrudDeleteComponent } from '../../generic-crud-delete/generic-crud-delete.component';
import { IconDefinition } from '@fortawesome/free-solid-svg-icons';

export abstract class ListComponent<T> implements IListComponent<T> {

    private itemsSubject: BehaviorSubject<DataItems<T>> = new BehaviorSubject<DataItems<T>>(new DataItems<T>());

    items: Observable<DataItems<T>> = this.itemsSubject.asObservable();
    modalRef: BsModalRef;
    actions: TableAction[] = [];
    supportsCreateUpdate = true;
    loadingItems = false;
    errors = [];
    itemsPerPage = 10;
    currentPage = 1;

    constructor(private modalService: BsModalService,
                private crudService: ICrudService<T>) {
                    this.addActions();
                }

    load(pageNumber: number): void {

        if (!this.canLoad(this.getOptions())) {
            return;
        }

        this.currentPage = pageNumber;
        this.errors = [];

        this.crudService.items.subscribe(items => {
            this.itemsSubject.next(items);
            this.loadingItems = false;
        });

        this.loadingItems = true;
        this.crudService.load(this.currentPage, this.itemsPerPage, this.getOptions()).subscribe(response => {
            this.loadingItems = false;
        }, (error: any) => {
            this.loadingItems = false;
            this.errors.push(error.message);
        });
    }

    openAddModel() {
        const initialState = this.getAddModelIntialState();
        this.modalRef = this.modalService.show(this.getAddModalContent(), {initialState});
    }

    openEditModal(item: T) {
        const initialState = this.getEditModelIntialState(item);
        this.modalRef = this.modalService.show(this.getEditModalContent(), {initialState});
    }

    openRemoveModal(item: T): void {
        const initialState = {
            deleteComponent: this.getDeleteComponent(),
            model: item
        };
        this.modalRef = this.modalService.show(GenericCrudDeleteComponent, {initialState});
    }

    canLoad(options: any): boolean {
        return true;
    }

    abstract getAddModalContent(): any;
    abstract getAddModelIntialState(): any;
    abstract getEditModalContent(): any;
    abstract getEditModelIntialState(item: T): any;
    abstract getDeleteComponent(): IDeleteComponent<T>;
    abstract convertToRow(item: T): Row;
    abstract addActions(): void;
    abstract getOptions(): any;
    abstract getTableColumns(): string[];
    abstract getTitle(): string;
    abstract getIcon(): IconDefinition;
}
