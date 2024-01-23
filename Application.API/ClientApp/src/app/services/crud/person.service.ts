import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Person } from 'src/app/models/person';
import { ResponseMessage } from 'src/app/models/response';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PersonService {
public baseUrl: string = ''
constructor(private http: HttpClient) { 
  this.baseUrl = `${environment.apiUrl}/api/person`;
}

getAllPersons(): Observable<Person[]>{
  return this.http.get<Person[]>(`${this.baseUrl}/getAll`);
}

addPerson(person: Person): Observable<ResponseMessage>{
  return this.http.post<ResponseMessage>(`${this.baseUrl}/post`,person);
}

updatePerson(id: string, person: Person): Observable<ResponseMessage>{
  return this.http.put<ResponseMessage>(`${this.baseUrl}/update`, person);
}

deletePerson(data: Person){
  return this.http.delete<ResponseMessage>(`${this.baseUrl}/delete`, {body: data});
}

}
