import { Guid } from 'guid-typescript';
import { IModels } from '../shared/crud-service/IDataSource';

export class Charity {
    id: number;
    charityIdentifier: Guid;
    charityName: string;
    contactPerson: string;
    contactNumber: string;
    emailAddress: string;
    updatedAt: Date;
}

export class Charities implements IModels<Charity> {
    offset: number;
    count: number;
    total: number;
    items: Charity[];
}
