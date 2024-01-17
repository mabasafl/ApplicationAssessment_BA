import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BusinessArea } from 'src/app/models/business-area';
import { BusinessAreaFiltering } from 'src/app/models/business-area-filtering';
import { Customer } from 'src/app/models/customer';
import { ResponseMessage } from 'src/app/models/response';
import { BusinessAreaRelationshipService } from 'src/app/services/crud/business-area-relationship.service';
import { BusinessAreaService } from 'src/app/services/crud/business-area.service';
import { CustomerService } from 'src/app/services/crud/customer.service';

@Component({
  selector: 'app-business-area-relationship-form',
  templateUrl: './business-area-relationship-form.component.html',
  styleUrls: ['./business-area-relationship-form.component.scss']
})
export class BusinessAreaRelationshipFormComponent implements OnInit {

  form!: FormGroup;
  businessAreaRelationships: BusinessAreaFiltering[] = [];
  existingBusinessAreaValue!: number;
  existingFilteredBusinessAreaValue!: number;
  existingCustomerValue!: number;
  customers: Customer[] = [];
  businessAreas: BusinessArea[] = [];
  filteredBusinessAreas: BusinessArea[] = [];
  responseMessage: ResponseMessage | null = null;

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<BusinessAreaRelationshipFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data: BusinessAreaFiltering, 
     private businessAreaRelationshipService: BusinessAreaRelationshipService,
     private customerService:CustomerService,
     private businessAreaService: BusinessAreaService) {
      }

  ngOnInit() {
    this.form = this.fb.group({
      name: new FormControl(''),
      businessAreaId: new FormControl(0),
      customerId: new FormControl(0),
      filteredBusinessAreaId: new FormControl(0),
      createdBy: new FormControl(''),      
      dateCreated: new FormControl(new Date()),
      dateModified: new FormControl(new Date()),
      modifiedBy:new FormControl(''),
      businessAreaName:new FormControl(''),
      filteredBusinessAreaName: new FormControl(''),
      customerName: new FormControl(''),
      customer: this.fb.group({
        name: new FormControl(''),
        createdBy: new FormControl('')
      })
    });

    this.form.patchValue(this.data);

    this.getBusinessAreas();

    this.getCustomers();

    this.getBusinessAreaRelationships();

    if(Number(this.form.get('businessAreaId')?.value) == 0){
      this.form.get('filteredBusinessAreaId')?.disable();
      this.businessAreas = this.getBusinessAreas();
    }

    this.form.get('businessAreaId')?.valueChanges.subscribe((businessAreaId) => {
      this.form.get('filteredBusinessAreaId')?.enable();
      this.filteredBusinessAreas = this.businessAreas.filter(x => x.id != businessAreaId);
    })

    
  }

  onSubmit(){

    /*.existingBusinessAreaValue = this.form.get('businessAreaId')?.value;
    this.existingFilteredBusinessAreaValue = this.form.get('filteredBusinessAreaId')?.value;
    this.existingCustomerValue = this.form.get('customerId')?.value;

    var exist = this.businessAreaRelationships.filter(x => x.businessAreaId == this.existingBusinessAreaValue && 
      x.filteredBusinessAreaId == this.existingFilteredBusinessAreaValue &&
      x.customerId == this.existingCustomerValue)

    if(exist){
      this.form.invalid;
    }*/

    if(this.form.invalid){
      this.responseMessage = {success: false, message:" form is not valid", timeStamp: new Date()}
    }

      if(this.data){
        console.log(this.form.value);
        this.businessAreaRelationshipService.updateBusinessAreaRelationship( this.form.value).subscribe({
          next:(response)  => {
            {
              if(response.success){
                alert('business area relationship updated.');
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
        this.businessAreaRelationshipService.addBusinessAreaRelationship(this.form.value).subscribe((response) =>{
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

  getBusinessAreas(): BusinessArea[]{
    this.businessAreaService.getAllBusinessAreas().subscribe((response) => {
      if(response){
        this.businessAreas = response;
      }
    })
    console.log('response:', this.businessAreas)
    return this.businessAreas;
  }

  getBusinessAreaRelationships(): BusinessAreaFiltering[]{
    this.businessAreaRelationshipService.getAllBusinessAreaRelationships().subscribe((response) =>{
      if(response){
        this.businessAreaRelationships = response;
      }
    })

    return this.businessAreaRelationships;
  }

}
