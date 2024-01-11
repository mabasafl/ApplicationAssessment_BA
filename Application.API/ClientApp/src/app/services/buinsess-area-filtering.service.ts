import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusinessAreaFiltering } from '../models/business-area-filtering';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { BusinessArea } from '../models/business-area';
import { BusinessAreaType } from '../models/business-area-type';

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

getAllBusinessAreaFiltering(id: number): Observable<BusinessAreaFiltering[]>{
  return this.http.get<BusinessAreaFiltering[]>(`${this.baseUrl}/businessAreaFiltering/getAll?businessAreaId=${id}`);
}

getData(): Observable<BusinessAreaFiltering[]>{
   return this.http.get<BusinessAreaFiltering[]>(`${this.baseUrl}/businessAreaFiltering/getAllData`);
}
}
