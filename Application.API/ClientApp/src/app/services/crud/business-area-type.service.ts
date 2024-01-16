import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BusinessAreaType } from 'src/app/models/business-area-type';
import { ResponseMessage } from 'src/app/models/response';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BusinessAreaTypeService {

  public baseUrl: string;
  constructor(private http: HttpClient) { 
    this.baseUrl = `${environment.apiUrl}/api/BusinessAreaType`;
  }
  
  getAllBusinessAreaTypes(): Observable<BusinessAreaType[]>{
    return this.http.get<BusinessAreaType[]>(`${this.baseUrl}/getAll`);
  }
  
  addBusinessAreaType(businessAreaType: BusinessAreaType): Observable<ResponseMessage>{
    return this.http.post<ResponseMessage>(`${this.baseUrl}/post`,businessAreaType)
  }
  
  updateBusinessAreaType(businessAreaType: BusinessAreaType): Observable<ResponseMessage>{
    return this.http.put<ResponseMessage>(`${this.baseUrl}/update`, businessAreaType)
  }
  
  deleteBusinessAreaType(){

}}
