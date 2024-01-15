import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of, takeUntil } from 'rxjs';
import { Application } from 'src/app/models/application';
import { ApplicationCustomers } from 'src/app/models/application-customer';
import { User } from 'src/app/models/user';
import { ApplicationService } from 'src/app/services/application.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  friendlyUrl!: string;
  application!: Application;
  isUrlValid: boolean = false;
  url!: string[];

  constructor(private router: Router, public authService: AuthService, 
    private applicationService: ApplicationService, 
    private activatedRoute: ActivatedRoute) {
}
  
  ngOnInit() {
    this.applicationService.getApplicationByName(this.activatedRoute.snapshot.paramMap.get('friendlyUrl') ?? '').subscribe((result) => {
      this.application = result;

      if(this.friendlyUrl == null || this.friendlyUrl == undefined || this.application.name == null || this.application.name != this.friendlyUrl){
        this.isUrlValid = false;
        this.router.navigateByUrl('/404')
        
      }else if(this.friendlyUrl == this.application.name){
        this.isUrlValid = true;
      }
    })

    if(window.location.href.includes('app')){
      sessionStorage.removeItem('url');
      sessionStorage.setItem('url',this.url[4])
    }
  }

  redirectToLogin(){
    this.router.navigateByUrl(`${this.friendlyUrl}/login`);
  }

  logout(){
    this.authService.logout();
    window.location.reload();
  }
}
