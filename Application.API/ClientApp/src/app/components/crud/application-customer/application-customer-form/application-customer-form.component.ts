import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Application } from 'src/app/models/application';
import { ApplicationCustomers } from 'src/app/models/application-customer';
import { Customer } from 'src/app/models/customer';
import { ResponseMessage } from 'src/app/models/response';
import { User } from 'src/app/models/user';
import { ApplicationService } from 'src/app/services/application.service';
import { ApplicationCrudService } from 'src/app/services/crud/application-crud.service';
import { ApplicationCustomerService } from 'src/app/services/crud/application-customer.service';
import { CustomerService } from 'src/app/services/crud/customer.service';

@Component({
  selector: 'app-application-customer-form',
  templateUrl: './application-customer-form.component.html',
  styleUrls: ['./application-customer-form.component.scss']
})
export class ApplicationCustomerFormComponent implements OnInit {


  form!: FormGroup;
  existingCustomerValue!: number;
  customers: Customer[] = [];
  applications: Application[] = [];
  applicationcustomers: ApplicationCustomers[] = [];
  responseMessage: ResponseMessage | null = null;
  userLogged!: User;

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<ApplicationCustomerFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data: ApplicationCustomers,
     private customerService:CustomerService,
     private applicationService: ApplicationCrudService,
     private applicationCustomerService: ApplicationCustomerService) {
      var user = sessionStorage.getItem('user');
      if(user != null){
        this.userLogged = JSON.parse(user)
      }
      }

  ngOnInit() {
    this.form = this.fb.group({
      applicationId: new FormControl(0),
      applicationName: new FormControl(''),
      customerId: new FormControl(0),
      customerName: new FormControl(''),
      createdBy: new FormControl(this.userLogged.userName),      
      dateCreated: new FormControl(new Date()),
      dateModified: new FormControl(new Date()),
      modifiedBy:new FormControl(''),
      isActive: new FormControl(true),
    });

    this.form.patchValue(this.data);

    this.getApplications();

    this.getCustomers();

  

    
  }

  onSubmit(){
    if(this.form.invalid){
      this.responseMessage = {success: false, message:" form is not valid", timeStamp: new Date()}
    }

      if(this.data){
        console.log(this.form.value);
        this.applicationCustomerService.updateApplicationCustomer( this.form.value).subscribe({
          next:(response)  => {
            {
              if(response.success){
                alert('application customer updated.');
                this.dialogRef.close(true);
              }
              else{
                this.responseMessage = response;
              }
            }
          },error:(err) => {
            this.responseMessage = {
              success: false,
              message:`An error occured. ${err}`, 
              timeStamp: new Date('yyy-MM-dd')
            };

          },
        })
      }else{
        this.applicationCustomerService.addApplicationCustomer(this.form.value).subscribe((response) =>{
          if(response.success){
            this.form.reset;
            this.dialogRef.close(true);
          }else{
            this.responseMessage = response;
          }
        })
      }
  }

  getCustomers(){
    this.customerService.getAllCustomers().subscribe((response) =>{
      if(response){
        this.customers = response;
      }
    })
  }

  getApplications(): Application[]{
    this.applicationService.getAllApplications().subscribe((response) => {
      if(response){
        this.applications = response;
      }
    })
    return this.applications;
  }
  

}
