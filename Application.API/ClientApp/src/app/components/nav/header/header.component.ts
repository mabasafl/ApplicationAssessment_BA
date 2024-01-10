import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { error } from 'console';
import { Observable, of } from 'rxjs';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private router: Router, public authService: AuthService) {   
}

  
  ngOnInit() {
    
  }

  redirectToLogin(){
    this.router.navigateByUrl("/login");
  }

  logout(){
    this.authService.logout();
    window.location.reload();
  }
}
