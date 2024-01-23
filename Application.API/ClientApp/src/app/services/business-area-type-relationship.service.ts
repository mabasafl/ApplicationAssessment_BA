import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BusinessAreaTypeRelationship } from '../models/business-area-type-relationship';
import { ResponseMessage } from '../models/response';

@Injectable({
  providedIn: 'root'
})
export class BusinessAreaTypeRelationshipService {

  public baseUrl:string;

constructor(private http: HttpClient) { 
  this.baseUrl = `${environment.apiUrl}/api/BusinessAreaTypeRelationship`;
}

getAllBusinessAreaTypeRelationship(customerId: number): Observable<BusinessAreaTypeRelationship[]>{
  return this.http.get<BusinessAreaTypeRelationship[]>(`${this.baseUrl}/getAll?customerId=${customerId}`);
}

getBusinessAreaTypeRelationship(businessAreaId: number): Observable<BusinessAreaTypeRelationship>{
  return this.http.get<BusinessAreaTypeRelationship>(`${this.baseUrl}/get?businessAreaId=${businessAreaId}`);
}

addBusinessAreaTypeRelationship(businessAreaTypeRelationship: BusinessAreaTypeRelationship){
  return this.http.post<ResponseMessage>(`${this.baseUrl}/post`,businessAreaTypeRelationship);
}

updateBusinessAreaTypeRelationship(businessAreaTypeRelationship: BusinessAreaTypeRelationship){
  return this.http.put<ResponseMessage>(`${this.baseUrl}/put`,businessAreaTypeRelationship);
}

deleteBusinessAreaTypeRelationship(data: BusinessAreaTypeRelationship){
  return this.http.delete<ResponseMessage>(`${this.baseUrl}/delete`,{body: data});
}


}
