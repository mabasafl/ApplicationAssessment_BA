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
  url: string = '';
  staticDefaultUrl = 'http://localhost:4200/';
  isDefaultUrl: boolean = false;
 constructor(private authService: AuthService){

 }
  ngOnInit() {
    this.setCurrentUser();
    if(this.url == this.staticDefaultUrl) this.isDefaultUrl = true;
    else this.isDefaultUrl = false;

    console.log('default url: ', this.isDefaultUrl)
  }

  setCurrentUser(){
    const sessionUser = sessionStorage.getItem('user');

    if(!sessionUser) return;

    const user: User = JSON.parse(sessionUser);
    this.authService.setCurrentUser(user);
  }
  
}
