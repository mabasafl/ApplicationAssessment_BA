import { inject } from '@angular/core/testing';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup} from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BusinessArea } from 'src/app/models/business-area';
import { Customer } from 'src/app/models/customer';
import { Person } from 'src/app/models/person';
import { BusinessAreaService } from 'src/app/services/crud/business-area.service';
import { CustomerService } from 'src/app/services/crud/customer.service';
import { PersonService } from 'src/app/services/crud/person.service';
import { User } from 'src/app/models/user';
import { ResponseMessage } from 'src/app/models/response';

@Component({
  selector: 'app-person-form',
  templateUrl: './person-form.component.html',
  styleUrls: ['./person-form.component.scss']
})
export class PersonFormComponent implements OnInit {
  form!: FormGroup;
  customers: Customer[] = [];
  businessAreas: BusinessArea[] = [];

  location: BusinessArea[] = [];
  department: BusinessArea[] = [];
  industry: BusinessArea[] = [];
  userLogged!: User;

  responseMessage: ResponseMessage | null = null;


  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<PersonFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data: Person, 
     private personService: PersonService,
     private customerService: CustomerService,
     private businessAreaService: BusinessAreaService) {
      var user = sessionStorage.getItem('user');
      if(user != null){
        this.userLogged = JSON.parse(user)
      }
      }

  ngOnInit() {

    this.form = this.fb.group({
      id: new FormControl(0),
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      emailAddress: new FormControl(''),
      mobileNumber: new FormControl(''),
      customerId: new FormControl(0),
      businessArea1: new FormControl(0),
      businessArea2: new FormControl(0),
      businessArea3: new FormControl(0),
      createdBy: [this.userLogged.userName],
      dateCreated: new FormControl(new Date()),
      dateModified: new FormControl(new Date()),
    });

    this.form.patchValue(this.data);

    this.getCustomers();

    this.getBusinessAreas();
  }

  onSubmit(){
    if(this.form.valid){
      if(this.data){
        this.personService.updatePerson(this.data.firstName, this.form.value).subscribe((response) =>{
          if(response.success){
            alert('person updated.');
            this.dialogRef.close(true);
          }
          else{
            alert('person not updated successfully.');
            this.responseMessage = response;
          }
        })
      }else{
        this.personService.addPerson(this.form.value).subscribe((response) =>{
          if(response.success){
            alert('person added');
            this.form.reset;
            this.dialogRef.close(true);
          }else{
            
            this.responseMessage = response;
            alert('person notadded successfully.');
          }
        })
      }
    }
  }

  getCustomers(){
    this.customerService.getAllCustomers().subscribe((response) =>{
      if(response){
        this.customers = response;
      }
    })
  }

  getBusinessAreas(){
    this.businessAreaService.getAllBusinessAreas().subscribe((response) => {
      if(response){

        this.businessAreas = response;
      }
    })
  }

  

}
