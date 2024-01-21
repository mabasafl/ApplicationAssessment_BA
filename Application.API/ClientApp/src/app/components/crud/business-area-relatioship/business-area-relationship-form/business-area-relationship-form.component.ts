import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
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
  businessAreaMatch: BusinessArea[] = [];
  filteredBusinessAreas: BusinessArea[] = [];
  filteredDepartmentBusinessAreas: BusinessArea[] = [];
  filteredIndustryBusinessAreas: BusinessArea[] = [];
  filteredLocationBusinessAreas: BusinessArea[] = [];
  businessAreaTypeRelationships!: BusinessAreaTypeRelationship;
  responseMessage!: ResponseMessage;
  userLogged!: User;
  fba2 !: number;
  fba3 !: number;
  fba1 !: number;


  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<BusinessAreaRelationshipFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data: BusinessAreaFiltering, 
     private businessAreaRelationshipService: BusinessAreaRelationshipService,
     private customerService:CustomerService,
     private businessAreaService: BusinessAreaService,
     private businessAreaTypeRelationshipService: BusinessAreaTypeRelationshipService) {
      var user = sessionStorage.getItem('user');
      if(user != null){
        this.userLogged = JSON.parse(user)
      }
      }

  ngOnInit() {
    this.form = this.fb.group({
      id: new FormControl(0),
      name: new FormControl(''),
      businessAreaId: new FormControl(0),
      customerId: new FormControl(0),
      filteredBusinessAreaId: new FormControl(0),
      filteredBusinessAreaId2: new FormControl(0),
      filteredBusinessAreaId3: new FormControl(0),
      createdBy: new FormControl(this.userLogged.userName),      
      dateCreated: new FormControl(new Date()),
      dateModified: new FormControl(new Date()),
      modifiedBy:new FormControl(''),
      businessAreaName:new FormControl(''),
      filteredBusinessAreaName: new FormControl(''),
      isActive: new FormControl(true),
      customerName: new FormControl(''),
      customer: this.fb.group({
        name: new FormControl(''),
        createdBy: new FormControl('')
      })
    });

    this.form.patchValue(this.data);

    this.getBusinessAreas();

    this.getCustomers();

    this.form.get('filteredBusinessAreaId2')?.valueChanges.subscribe((fba2) =>{
      this.fba2 = Number(fba2);
    })
    this.form.get('filteredBusinessAreaId3')?.valueChanges.subscribe((fba3) =>{
      this.fba3 = Number(fba3);
    })

    if(Number(this.form.get('businessAreaId')?.value) == 0){
      this.form.get('filteredBusinessAreaId')?.disable();
      this.businessAreas = this.getBusinessAreas();
    }

    this.form.get('businessAreaId')?.valueChanges.subscribe((businessAreaId) => {
      this.form.get('filteredBusinessAreaId')?.enable();
      this.businessAreaTypeRelationships = this.getBusinessAreaTypeRelationship(businessAreaId);    
      this.filteredBusinessAreas = this.businessAreas.filter(x => x.id != businessAreaId);
      this.filteredDepartmentBusinessAreas = this.filteredBusinessAreas.filter(x => x.businessAreaTypeId == 2)
      this.filteredIndustryBusinessAreas = this.filteredBusinessAreas.filter(x => x.businessAreaTypeId == 3)
      this.filteredLocationBusinessAreas = this.filteredBusinessAreas.filter(x => x.businessAreaTypeId == 1)
    })    
  }

  onSubmit(){
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
               
        if(this.fba2 != 0 || this.fba2 != undefined){
        this.form.setControl("filteredBusinessAreaId",new FormControl(this.fba2))
         this.addForm();
        }

        
        if(this.fba3 != 0 || this.fba3 != undefined){
        this.form.setControl("filteredBusinessAreaId",new FormControl(this.fba3))
         this.addForm();
        }

        this.form.reset;
        this.dialogRef.close(true);   
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

  getBusinessAreaTypeRelationship(businessAreaId: number): BusinessAreaTypeRelationship{
    this.businessAreaTypeRelationshipService.getBusinessAreaTypeRelationship(businessAreaId).subscribe((response) => {
      if(response){
        this.businessAreaTypeRelationships = response;
        console.log('business area type relationshp', this.businessAreaTypeRelationships)

      }
    })
    return this.businessAreaTypeRelationships;
  }

  addForm(){
    this.businessAreaRelationshipService.addBusinessAreaRelationship(this.form.value).subscribe((response) =>{
        this.responseMessage = response;
    });
  }

}
