import { Guid } from 'guid-typescript';
import { IModels } from '../shared/crud-service/IDataSource';

export class DonorCharity {
    charityIdentifier: Guid;
    donorId: number;
    charityName: string;
    donationPercentage: number;
}

export class DonorCharities implements IModels<DonorCharity> {
    items: DonorCharity[];
    offset: number;
    count: number;
    total: number;
}
