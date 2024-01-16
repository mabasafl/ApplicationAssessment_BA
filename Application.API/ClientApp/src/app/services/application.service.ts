import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApplicationCustomers } from '../models/application-customer';
import { Application } from '../models/application';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  public baseUrl:string ;

constructor(private http: HttpClient) {
  this.baseUrl = `${environment.apiUrl}/api`
 }

 getApplication(friendlyUrl?: string): Observable<ApplicationCustomers[]>{
  return this.http.get<ApplicationCustomers[]>(`${this.baseUrl}/ApplicationCustomers/${friendlyUrl}`);
 }

 getApplicationByName(friendlyurl?: string): Observable<Application>{
  return this.http.get<Application>(`${this.baseUrl}/Application/getByName?name=${friendlyurl}`);
 }

 

}
