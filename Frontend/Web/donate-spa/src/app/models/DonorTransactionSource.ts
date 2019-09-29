import { IModels } from '../shared/crud-service/IDataSource';

export class DonorTransactionSource {
    id: number;
    financialInstitution: string;
    type: string;
    identifier: string;
    donorId: number;
}

export class DonorTransactionSources implements IModels<DonorTransactionSource> {
    items: DonorTransactionSource[];
    offset: number;
    count: number;
    total: number;
}
