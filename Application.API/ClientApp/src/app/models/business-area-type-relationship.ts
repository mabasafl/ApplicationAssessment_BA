export interface BusinessAreaTypeRelationship{
    id: number;
    customerId: number;
    customerName: string;
    businessAreaId: number;
    businessAreaName: string;
    businessAreaType1: boolean;
    businessAreaType2: boolean;
    businessAreaType3: boolean;
    isActive: boolean;
    dateCreated: Date;
    createdBy: string;
    dateModified: Date;
    modifiedBy: string;
}