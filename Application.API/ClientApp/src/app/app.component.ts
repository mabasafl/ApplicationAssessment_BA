import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';
 constructor(private authService: AuthService){

 }
  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser(){
    const sessionUser = sessionStorage.getItem('user');

    if(!sessionUser) return;

    const user: User = JSON.parse(sessionUser);
    this.authService.setCurrentUser(user);
  }
  
}
