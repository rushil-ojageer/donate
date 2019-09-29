interface IDictionary {
    [index: string]: any;
}

export class DataTable {
    columns: string[];
    rows: Row[];
}

export class Row {
    private row: IDictionary = {} as IDictionary;
    entity: any;

    constructor(entity: any) {
        this.entity = entity;
    }

    addCell(column: string, value: any) {
        this.row[column] = value;
    }

    getCell(column: string): any {
        return this.row[column];
    }
}
