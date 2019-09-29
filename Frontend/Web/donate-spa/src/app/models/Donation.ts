import { IModels } from '../shared/crud-service/IDataSource';


export class Donation {
    id: number;
    donorId: number;
    charity: string;
    donor: string;
    merchant: string;
    amount: number;
    transactionAmount: number;
    currency: string;
    donationDateTimeUtc: Date;
    transactionDateTimeUtc: Date;
}

export class Donations implements IModels<Donation> {
    items: Donation[];
    offset: number;
    count: number;
    total: number;
}
