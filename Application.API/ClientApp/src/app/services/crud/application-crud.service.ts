import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Application } from 'src/app/models/application';
import { ResponseMessage } from 'src/app/models/response';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApplicationCrudService {

  public baseUrl:string ;

constructor(private http: HttpClient) {
  this.baseUrl = `${environment.apiUrl}/api/Application`
 }

 getApplicationByName(friendlyurl?: string): Observable<Application>{
  return this.http.get<Application>(`${this.baseUrl}/getByName?name=${friendlyurl}`);
 }

 getAllApplications(): Observable<Application[]>{
  return this.http.get<Application[]>(`${this.baseUrl}/getAll`);
 }
 getApplication(applicationId: number): Observable<Application>{
  return this.http.get<Application>(`${this.baseUrl}/get?id=${applicationId}`);
 }

 addApplication(application: Application): Observable<ResponseMessage>{
  return this.http.post<ResponseMessage>(`${this.baseUrl}/post`,application)
}

updateApplication(application: Application): Observable<ResponseMessage>{
  return this.http.put<ResponseMessage>(`${this.baseUrl}/update`, application)
}

deleteApplication(data: Application){
  return this.http.delete<ResponseMessage>(`${this.baseUrl}/delete`,{body: data});
}}
