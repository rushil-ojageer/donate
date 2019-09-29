import { Component, OnInit, Input } from '@angular/core';
import { IconDefinition, faSpinner } from '@fortawesome/free-solid-svg-icons';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { DataTable, Row } from 'src/app/models/DataTable';
import { Observable } from 'rxjs';
import { IListComponent } from './helper-classes/IListComponent';

@Component({
  selector: 'app-generic-crud-list',
  templateUrl: './generic-crud-list.component.html',
  styleUrls: ['./generic-crud-list.component.less']
})
export class GenericCrudListComponent implements OnInit {

  constructor() {
  }

  @Input() listComponent: IListComponent<any>;

  title: string;
  icon: IconDefinition;
  faPlus = faPlus;
  faSpinner = faSpinner;
  dataTable: DataTable = new DataTable();
  showDirectionLinks = true;
  showBoundaryLinks = true;
  maxPageSize = 5;
  columns: string[];
  totalItems = 0;
  itemsPerPage = 0;
  currentPage = 1;
  data = [];
  actions: TableAction[] = [];

  ngOnInit() {
    this.title = this.listComponent.getTitle();
    this.icon = this.listComponent.getIcon();
    this.actions = this.listComponent.actions;
    this.columns = this.listComponent.getTableColumns();
    this.listComponent.load(1);
    this.buildDataTable();
  }

  openAddModel(): void {
    this.listComponent.openAddModel();
  }

  pageChanged(event: any): void {
    if (event.page === this.currentPage) {
      return;
    }
    this.listComponent.load(event.page);
  }

  onActionClick(action: TableAction, item: Row) {
    if (action.hasProgressBar) {
      action.showProgress = true;
      action.action(item.entity);
      action.showProgress = false;
    }
    action.action(item.entity);
  }

  getColumnCount(): number {
    let columnCount = this.dataTable.columns.length;
    if (this.actions.length > 0) {
      columnCount += 1;
    }
    return columnCount;
  }

  isLoading(): boolean {
    return this.listComponent.loadingItems;
  }

  hasErrors(): boolean {
    return this.listComponent.errors.length > 0;
  }

  private buildDataTable() {
    this.dataTable.columns = this.columns;
    this.dataTable.rows = [];
    this.listComponent.items.subscribe(dataItems => {
      this.data = dataItems.items;
      this.totalItems = dataItems.totalItems;
      this.itemsPerPage = dataItems.itemsPerPage;
      this.currentPage = dataItems.currentPage;
      this.dataTable.rows = [];
      this.data.forEach(item => {
        this.dataTable.rows.push(this.listComponent.convertToRow(item));
      });
    });
  }
}

export class TableAction {
  label: string;
  hasProgressBar: boolean;
  showProgress: boolean;
  action: (x: any) => void;
}
