import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationCustomers } from 'src/app/models/application-customer';
import { User } from 'src/app/models/user';
import { ApplicationService } from 'src/app/services/application.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {

  friendlyUrl!: string;
  applications: ApplicationCustomers[] = [];
  application!: ApplicationCustomers;
  isUrlValid: boolean = false;
  url!: string[];
  isLoggedIn: boolean = false;


  constructor(private router: Router, public authService: AuthService, 
    private applicationService: ApplicationService, 
    private activatedRouute: ActivatedRoute) {   
    this.friendlyUrl = this.router.url.slice(1);
    
    this.url = this.router.url.split('/');
    this.friendlyUrl = this.url[2];
}

  ngOnInit() {
    this.applicationService.getApplication(this.activatedRouute.snapshot.paramMap.get('friendlyUrl') ?? '').subscribe((result) => {
      this.applications = result;

      if(this.friendlyUrl == null || this.friendlyUrl == undefined || this.applications == null){
        this.isUrlValid = false;
        this.router.navigateByUrl('404');
      }else if(this.friendlyUrl == this.applications[1].friendlyUrl){
        this.isUrlValid = true;
      }
    })

    if(window.location.href.includes('app')){
      sessionStorage.removeItem('url');
      sessionStorage.setItem('url',this.url[4])
    }

    this.userSignedIn()

  }

  userSignedIn(){
    var user = sessionStorage.getItem('user');
    if(user != null){
      this.isLoggedIn = true;
      
    }else{
      this.isLoggedIn;
    }
    
  }


}
