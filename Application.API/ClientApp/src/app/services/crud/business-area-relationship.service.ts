import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BusinessAreaFiltering } from 'src/app/models/business-area-filtering';
import { ResponseMessage } from 'src/app/models/response';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BusinessAreaRelationshipService {

  public baseUrl:string ;

constructor(private http: HttpClient) {
  this.baseUrl = `${environment.apiUrl}/api/BusinessAreaFiltering`
 }

 getAllBusinessAreaRelationships(): Observable<BusinessAreaFiltering[]>{
  return this.http.get<BusinessAreaFiltering[]>(`${this.baseUrl}/getAllData`);
 }
 getBusinessAreaRelationship(businessAreaRelationshipId: number): Observable<BusinessAreaFiltering>{
  return this.http.get<BusinessAreaFiltering>(`${this.baseUrl}/get?id=${businessAreaRelationshipId}`);
 }

 addBusinessAreaRelationship(businessAreaRelationship: BusinessAreaFiltering): Observable<ResponseMessage>{
  return this.http.post<ResponseMessage>(`${this.baseUrl}/post`,businessAreaRelationship);
}

updateBusinessAreaRelationship(businessAreaRelationship: BusinessAreaFiltering): Observable<ResponseMessage>{
  return this.http.put<ResponseMessage>(`${this.baseUrl}/update`, businessAreaRelationship);
}

deleteBusinessAreaRelationship(data: BusinessAreaFiltering){
  return this.http.delete<ResponseMessage>(`${this.baseUrl}/delete`,{body: data});
}

}
