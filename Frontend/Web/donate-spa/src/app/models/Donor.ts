import { Guid } from 'guid-typescript';
import { IModels } from '../shared/crud-service/IDataSource';

export class Donor {
    id: number;
    firstName: string;
    lastName: string;
    identityType: string;
    identityNumber: string;
    contactNumber: string;
    emailAddress: string;
    updatedAt: Date;
    transactionDonationPercentage: number;
    donationCap: number;
}

export class Donors implements IModels<Donor> {
    items: Donor[];
    offset: number;
    count: number;
    total: number;
}
