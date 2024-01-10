import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';
import { AuthResult } from '../models/auth-results';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public baseUrl:string ;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

constructor(private http: HttpClient) 
{ 
  this.baseUrl = `${environment.apiUrl}/api/auth`
}

login(user: User){
  return this.http.post<AuthResult>(`${this.baseUrl}/login`,user)
    .pipe(
      map((res: AuthResult) =>{
        const signedUser = res;
        if(signedUser.success == true){
          sessionStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user)

          sessionStorage.setItem('token',res.token);

        }
      })
    )
}

setCurrentUser(user: User){
  this.currentUserSource.next(user);
}

logout(){
  sessionStorage.removeItem("user");
  this.currentUserSource.next(null);
  return this.http.post(`${this.baseUrl}/logout`,null);
}

}
