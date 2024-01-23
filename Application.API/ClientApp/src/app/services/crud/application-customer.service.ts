import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApplicationCustomers } from 'src/app/models/application-customer';
import { ResponseMessage } from 'src/app/models/response';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApplicationCustomerService {

  public baseUrl:string ;

constructor(private http: HttpClient) {
  this.baseUrl = `${environment.apiUrl}/api/ApplicationCustomers`
 }

/* getApplicationByName(friendlyurl?: string): Observable<Application>{
  return this.http.get<Application>(`${this.baseUrl}/getByName?name=${friendlyurl}`);
 }*/

 getAllApplicationsCustomers(): Observable<ApplicationCustomers[]>{
  return this.http.get<ApplicationCustomers[]>(`${this.baseUrl}/getAll`);
 }

 addApplicationCustomer(applicationCustomer: ApplicationCustomers): Observable<ResponseMessage>{
  return this.http.post<ResponseMessage>(`${this.baseUrl}/post`,applicationCustomer)
}

updateApplicationCustomer(applicationCustomer: ApplicationCustomers): Observable<ResponseMessage>{
  return this.http.put<ResponseMessage>(`${this.baseUrl}/update`, applicationCustomer)
}

deleteApplicationCustomer(data: ApplicationCustomers){
  return this.http.delete<ResponseMessage>(`${this.baseUrl}/delete`,{body: data});
}

}
