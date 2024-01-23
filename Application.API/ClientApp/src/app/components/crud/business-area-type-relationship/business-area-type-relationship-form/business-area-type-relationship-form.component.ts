import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BusinessArea } from 'src/app/models/business-area';
import { BusinessAreaFiltering } from 'src/app/models/business-area-filtering';
import { BusinessAreaTypeRelationship } from 'src/app/models/business-area-type-relationship';
import { Customer } from 'src/app/models/customer';
import { ResponseMessage } from 'src/app/models/response';
import { User } from 'src/app/models/user';
import { BusinessAreaTypeRelationshipService } from 'src/app/services/business-area-type-relationship.service';
import { BusinessAreaRelationshipService } from 'src/app/services/crud/business-area-relationship.service';
import { BusinessAreaService } from 'src/app/services/crud/business-area.service';
import { CustomerService } from 'src/app/services/crud/customer.service';

@Component({
  selector: 'app-business-area-type-relationship-form',
  templateUrl: './business-area-type-relationship-form.component.html',
  styleUrls: ['./business-area-type-relationship-form.component.scss']
})
export class BusinessAreaTypeRelationshipFormComponent implements OnInit {

  form!: FormGroup;
  businessAreaTypeRelationships: BusinessAreaTypeRelationship[] = [];
  customers: Customer[] = [];
  businessAreas: BusinessArea[] = [];
  selectedBusinessArea !: BusinessArea;
  responseMessage: ResponseMessage | null = null;
  userLogged!: User;
  businessAreaId!: number;
  locationOption: boolean = false;
  departmentOption: boolean = false;
  industryOption: boolean = false;
  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<BusinessAreaTypeRelationshipFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data: BusinessAreaTypeRelationship, 
     private businessAreaTypeRelationshipService: BusinessAreaTypeRelationshipService,
     private businessAreaRelationshipService: BusinessAreaRelationshipService,
     private customerService:CustomerService,
     private businessAreaService: BusinessAreaService) {
      var user = sessionStorage.getItem('user');
      if(user != null){
        this.userLogged = JSON.parse(user)
      }
      }

  ngOnInit() {
    this.form = this.fb.group({
      id: new FormControl(0),
      businessAreaId: new FormControl(0),
      customerId: new FormControl(1),
      businessAreaType1: new FormControl(false),
      businessAreaType2: new FormControl(false),
      businessAreaType3: new FormControl(false),
      createdBy: new FormControl(this.userLogged.userName),      
      dateCreated: new FormControl(new Date()),
      dateModified: new FormControl(new Date()),
      modifiedBy:new FormControl(''),
      isActive: new FormControl(true),
      businessAreaName:'',
      customerName:''
    });

    this.form.patchValue(this.data);

    this.getBusinessAreas();
    this.getCustomers();

    this.form.get('businessAreaId')?.valueChanges.subscribe((businessAreaId) => {
      if(businessAreaId){
        this.selectedBusinessArea = this.getBusinessArea(businessAreaId);
      }
    })
    
  }

  onSubmit(){
    if(this.form.invalid){
      this.responseMessage = {success: false, message:" form is not valid", timeStamp: new Date()}
    }

      if(this.data){
        console.log(this.form.value);
        this.businessAreaTypeRelationshipService.updateBusinessAreaTypeRelationship( this.form.value).subscribe({
          next:(response)  => {
            {
              if(response.success){
                alert('business area type relationship updated.');
                this.dialogRef.close(true);
              }
              else{
                this.responseMessage = response;
                alert('business area type relationship was not updated.');
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
        this.businessAreaTypeRelationshipService.addBusinessAreaTypeRelationship(this.form.value).subscribe((response) =>{
          if(response.success){
            this.form.reset;
            this.dialogRef.close(true);
            alert("business area type relationship was added successfully.")
          }else{
            this.responseMessage = response;
            alert("business area type relationship was not added successfully.")
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

  getBusinessArea(businessAreaId: number): BusinessArea{
    this.businessAreaService.getBusinessArea(businessAreaId).subscribe((response) => {
      if(response){
        this.selectedBusinessArea= response;
        this.selectedBusinessArea?.businessAreaTypeId == 1 ? this.locationOption = true : this.locationOption = false;
        this.selectedBusinessArea?.businessAreaTypeId == 2 ? this.departmentOption = true : this.departmentOption = false
        this.selectedBusinessArea?.businessAreaTypeId == 3 ? this.industryOption = true : this.industryOption = false;
      }
    })
    return this.selectedBusinessArea;
  }

}
