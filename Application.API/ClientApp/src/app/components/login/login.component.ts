import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, private formBuilder: FormBuilder, private router: Router) { }

  loginForm = this.formBuilder.group({
    userName: [''],
    password: ['']
  });

  ngOnInit() {
  }

  onLogin() {
    this.authService.login(this.loginForm.value).subscribe({
      next: () => 
      {
        alert("you have signed in successfully");
        this.router.navigateByUrl('/');
      },
      error: () => alert("you were not signed in")
    })
  }

}
