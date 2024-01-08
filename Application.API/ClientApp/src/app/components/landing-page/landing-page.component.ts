import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {

  isLoggedIn: boolean = false;
  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.userSignedIn()
  }

  userSignedIn(){
    var user = localStorage.getItem('user');
    if(user != null){
      this.isLoggedIn = true;
      
    }else{
      this.isLoggedIn;
    }
    
  }


}
