import { inject } from '@angular/core/testing';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup} from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BusinessArea } from 'src/app/models/business-area';
import { Customer } from 'src/app/models/customer';
import { Person } from 'src/app/models/person';
import { BusinessAreaService } from 'src/app/services/crud/business-area.service';
import { CustomerService } from 'src/app/services/crud/customer.service';
import { PersonService } from 'src/app/services/crud/person.service';

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

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<PersonFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data: Person, 
     private personService: PersonService,
     private customerService: CustomerService,
     private businessAreaService: BusinessAreaService) {
      }

  ngOnInit() {

    this.form = this.fb.group({
      firstName: [''],
      lastName: [''],
      emailAddress: [''],
      contactNumber: [''],
      customerId: [0],
      businessArea1: [0],
      businessArea2: [0],
      businessArea3: [0],
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
          }
        })
      }else{
        this.personService.addPerson(this.form.value).subscribe((response) =>{
          if(response.success){
            alert('person added');
            this.form.reset;
            this.dialogRef.close(true);
          }else{
            alert('person notadded successfully.')
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

        this.location = response.filter(x => x.businessAreaTypeId == 1)
        console.log(this.location);

        this.department = response.filter(x => x.businessAreaTypeId == 2)
        console.log(this.department);

        this.industry = response.filter(x => x.businessAreaTypeId == 3)
        console.log(this.industry);
      }
    })
  }

  

}
