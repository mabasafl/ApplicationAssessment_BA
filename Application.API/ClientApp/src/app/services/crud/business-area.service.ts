import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BusinessArea } from 'src/app/models/business-area';
import { ResponseMessage } from 'src/app/models/response';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BusinessAreaService {

  public baseUrl: string;
  constructor(private http: HttpClient) { 
    this.baseUrl = `${environment.apiUrl}/api/businessArea`;
  }
  
  getAllBusinessAreas(): Observable<BusinessArea[]>{
    return this.http.get<BusinessArea[]>(`${this.baseUrl}/getAll`);
  }

  getBusinessArea(businessAreaId: number): Observable<BusinessArea>{
    return this.http.get<BusinessArea>(`${this.baseUrl}/get?id=${businessAreaId}`);
  }

  addBusinessArea(businessArea: BusinessArea): Observable<ResponseMessage>{
    return this.http.post<ResponseMessage>(`${this.baseUrl}/post`,businessArea)
  }
  
  updateBusinessArea(businessArea: BusinessArea): Observable<ResponseMessage>{
    return this.http.put<ResponseMessage>(`${this.baseUrl}/update`, businessArea)
  }
  
  deleteBusinessArea(){
    
  }

}
