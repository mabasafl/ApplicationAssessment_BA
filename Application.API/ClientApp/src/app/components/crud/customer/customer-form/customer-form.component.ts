import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Customer } from 'src/app/models/customer';
import { ResponseMessage } from 'src/app/models/response';
import { User } from 'src/app/models/user';
import { CustomerService } from 'src/app/services/crud/customer.service';

@Component({
  selector: 'app-customer-form',
  templateUrl: './customer-form.component.html',
  styleUrls: ['./customer-form.component.scss']
})
export class CustomerFormComponent implements OnInit {
  form!: FormGroup;
  customers: Customer[] = [];
  responseMessage: ResponseMessage | null = null;
  userLogged!: User;

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<CustomerFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data: Customer, 
     private customerService: CustomerService) {
      var user = sessionStorage.getItem('user');
      if(user != null){
        this.userLogged = JSON.parse(user)
      }
      }

  ngOnInit() {
    this.form = this.fb.group({
      id: new FormControl(0),
      name: new FormControl(''),
      createdBy: new FormControl(this.userLogged.userName),
      dateCreated: new FormControl(new Date()),
      dateModified: new FormControl(new Date()),
      modifiedBy:new FormControl('')
    });

    this.form.patchValue(this.data);
  }

  onSubmit(){
    if(this.form.valid){
      if(this.data){
        this.customerService.updateCustomer( this.form.value).subscribe({
          next:(response)  => {
            {
              if(response.success){
                alert('customer updated.');
                this.dialogRef.close(true);
              }
              else{
                this.responseMessage = response;
                console.log("resposne message 1", this.responseMessage);
              }
            }
          },error:(err) => {
            this.responseMessage = {
              success: false,
              message:`An error occured. ${err}`, 
              timeStamp: new Date('yyy-MM-dd')
            };

            console.log("resposne message 2", this.responseMessage);
          },
        })
      }else{
        this.customerService.addCustomer(this.form.value).subscribe((response) =>{
          if(response.success){
            alert('customer added');
            this.form.reset;
            this.dialogRef.close(true);
          }else{
            alert('customer not added successfully.')
          }
        })
      }
    }
    else{
      this.responseMessage = {success: false, message:" form is not valid", timeStamp: new Date()}
    }
  }
}
