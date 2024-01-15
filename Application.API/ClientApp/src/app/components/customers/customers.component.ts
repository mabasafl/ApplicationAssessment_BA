import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationCustomers } from 'src/app/models/application-customer';
import { ApplicationService } from 'src/app/services/application.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit {

  form!: FormGroup;
  friendlyUrl!: string;
  applications: ApplicationCustomers[] = [];
  application!: ApplicationCustomers;
  isUrlValid: boolean = false;
  url!: string[];
  currentCustomerId: number = 0;
  
  constructor(private router: Router, public authService: AuthService, 
    private applicationService: ApplicationService, 
    private activatedRouute: ActivatedRoute, private fb: FormBuilder) {   
    this.friendlyUrl = this.router.url.slice(1);
    
    this.url = this.router.url.split('/');
    this.friendlyUrl = this.url[2];
}

  ngOnInit() {
    
    this.form = this.fb.group({
      customers: new FormControl()
    });

    this.applicationService.getApplication(this.activatedRouute.snapshot.paramMap.get('friendlyUrl') ?? '').subscribe((result) => {
      this.applications = result;
      if(this.friendlyUrl == null || this.friendlyUrl == undefined || this.applications == null){
        this.isUrlValid = false;
      }else if(this.friendlyUrl == this.applications[1].friendlyUrl){
        this.isUrlValid = true;
      }
    })

    if(window.location.href.includes('app')){
      sessionStorage.removeItem('url');
      sessionStorage.setItem('url',this.url[4])
    }

    this.form.get('customers')?.valueChanges.subscribe((customer) =>{
      window.location.reload;
      if(this.form.get('customers')?.value){
        this.currentCustomerId = customer
        sessionStorage.removeItem('custId')
        sessionStorage.setItem('custId',this.currentCustomerId.toString())
      }else{
        this.currentCustomerId = 0;
      }
    }
    )
  }

}
