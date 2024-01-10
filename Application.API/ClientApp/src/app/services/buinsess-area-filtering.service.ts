import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusinessAreaFiltering } from '../models/business-area';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BuinsessAreaFilteringService {
baseUrl: string = '';
constructor(private http: HttpClient) { 
  this.baseUrl = `${environment.apiUrl}/api/businessAreaFiltering`
}

getAllBusinessAreaFiltering(id: number): Observable<BusinessAreaFiltering[]>{
  return this.http.get<BusinessAreaFiltering[]>(`${this.baseUrl}/getAll?businessArea=${id}`);
}
}
