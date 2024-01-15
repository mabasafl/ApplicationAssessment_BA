import { BusinessAreaFiltering } from 'src/app/models/business-area-filtering';
import { Customer } from './customer';
import { Person } from './person';

export interface CascadeFilter{
    businessAreaRelationship: BusinessAreaFiltering[];
    customer: Customer[];
    person: Person[];
}