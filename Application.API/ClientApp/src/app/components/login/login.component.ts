import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Application } from 'src/app/models/application';
import { ApplicationCustomers} from 'src/app/models/application-customer';
import { User } from 'src/app/models/user';
import { ApplicationService } from 'src/app/services/application.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  hel!: string |null;
  friendlyUrl!: string;
  application!: Application;
  isUrlValid: boolean = false;
  url!: string[];
  
  constructor(private authService: AuthService, private formBuilder: FormBuilder, private router: Router, private applicationService: ApplicationService, private activatedRoute: ActivatedRoute) { 
    this.friendlyUrl = this.router.url.slice(1);
    
    this.url = this.router.url.split('/');
    this.friendlyUrl = this.url[1];
  }

  loginForm = this.formBuilder.group({
    userName: [''],
    password: ['']
  });

  ngOnInit() {
    this.applicationService.getApplicationByName(this.activatedRoute.snapshot.paramMap.get('friendlyUrl') ?? '').subscribe((result) => {
      this.application = result;
      if(this.friendlyUrl == null || this.friendlyUrl == undefined || this.application.name == null || this.application.name != this.friendlyUrl){
        this.isUrlValid = false;       
      }else if(this.friendlyUrl == this.application.name){
        this.isUrlValid = true;
      }
    })

    if(window.location.href.includes('app')){
      sessionStorage.removeItem('url');
      sessionStorage.setItem('url',this.url[4])
    }
  }

  onLogin() {
    this.authService.login(this.loginForm.value).subscribe({
      next: () => 
      {
        alert("you have signed in successfully");
        this.router.navigateByUrl(`${this.friendlyUrl}/app`);
      },
      error: () => alert("you were not signed in")
    })
  }

}
