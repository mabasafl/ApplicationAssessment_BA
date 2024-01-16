import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from 'src/app/models/customer';
import { ResponseMessage } from 'src/app/models/response';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
public baseUrl: string;
constructor(private http: HttpClient) { 
  this.baseUrl = `${environment.apiUrl}/api/customer`;
}

getAllCustomers(): Observable<Customer[]>{
  return this.http.get<Customer[]>(`${this.baseUrl}/getAll`);
}

getCustomer(customerId: number): Observable<Customer>{
  return this.http.get<Customer>(`${this.baseUrl}/get?id=${customerId}`);
}

addCustomer(customer: Customer): Observable<ResponseMessage>{
  return this.http.post<ResponseMessage>(`${this.baseUrl}/post`,customer)
}

updateCustomer(customer : Customer): Observable<ResponseMessage>{
  return this.http.put<ResponseMessage>(`${this.baseUrl}/update`, customer)
}

deletePerson(){
  
}}
