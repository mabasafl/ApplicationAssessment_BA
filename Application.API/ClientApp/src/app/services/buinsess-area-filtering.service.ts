import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusinessAreaFiltering } from '../models/business-area-filtering';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { BusinessArea } from '../models/business-area';
import { BusinessAreaType } from '../models/business-area-type';
import { CascadeFilter } from '../models/cascade-filter';
import { Person } from '../models/person';

@Injectable({
  providedIn: 'root'
})
export class BuinsessAreaFilteringService {

public baseUrl:string ;

constructor(private http: HttpClient){
  this.baseUrl = `${environment.apiUrl}/api`
}

getBusinessAreaTypes(): Observable<BusinessAreaType[]>{
  return this.http.get<BusinessAreaType[]>(`${this.baseUrl}/businessAreaType/getAll`);
}

getAllBusinessAreaFiltering(businessAreaId: number, customerId: number): Observable<BusinessAreaFiltering[]>{
  return this.http.get<BusinessAreaFiltering[]>(`${this.baseUrl}/businessAreaFiltering/getAll?businessAreaId=${businessAreaId}&customerId=${customerId}`);
}

getData(): Observable<BusinessAreaFiltering[]>{
   return this.http.get<BusinessAreaFiltering[]>(`${this.baseUrl}/businessAreaFiltering/getAllData`);
}

getCascadingFilteringData(businessArea1: number, businessArea2: number, businessArea3: number, customerId: number,applicationId: number): Observable<Person[]>{
  return this.http.get<Person[]>(`${this.baseUrl}/businessAreaFiltering/getAllCascade?businessArea1=${businessArea1}&businessArea2=${businessArea2}&businessArea3=${businessArea3}&customerId=${customerId}&applicationId=${applicationId}`);
}

getDropDown(businessArea1: number, businessArea2: number, customerId: number,applicationId: number): Observable<BusinessAreaFiltering[]>{
  return this.http.get<BusinessAreaFiltering[]>(`${this.baseUrl}/businessAreaFiltering/getDropdown?businessArea1=${businessArea1}&businessArea2=${businessArea2}&customerId=${customerId}&applicationId=${applicationId}`);
}

}
