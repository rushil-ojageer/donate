import { Row } from 'src/app/models/DataTable';
import { Observable } from 'rxjs';
import { TableAction } from '../generic-crud-list.component';
import { IconDefinition } from '@fortawesome/free-solid-svg-icons';

export interface IListComponent<T> {
    errors: string[];
    items: Observable<DataItems<T>>;
    actions: TableAction[];
    loadingItems: boolean;
    supportsCreateUpdate: boolean;
    load(pageNumber: number): void;
    openAddModel(): void;
    openEditModal(item: T): void;
    openRemoveModal(item: T): void;
    convertToRow(item: T): Row;
    getTableColumns(): string[];
    getTitle(): string;
    getIcon(): IconDefinition;
}

export class DataItems<T> {

    constructor() {
        this.items = [];
        this.totalItems = 0;
        this.itemsPerPage = 0;
        this.currentPage = 1;
    }

    items: T[];
    totalItems: number;
    itemsPerPage: number;
    currentPage: number;
}
