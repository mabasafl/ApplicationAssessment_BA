import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BusinessArea } from 'src/app/models/business-area';
import { BusinessAreaType } from 'src/app/models/business-area-type';
import { Customer } from 'src/app/models/customer';
import { ResponseMessage } from 'src/app/models/response';
import { BusinessAreaTypeService } from 'src/app/services/crud/business-area-type.service';
import { BusinessAreaService } from 'src/app/services/crud/business-area.service';
import { CustomerService } from 'src/app/services/crud/customer.service';

@Component({
  selector: 'app-business-area-form',
  templateUrl: './business-area-form.component.html',
  styleUrls: ['./business-area-form.component.scss']
})
export class BusinessAreaFormComponent implements OnInit {

  form!: FormGroup;
  businessAreas: BusinessArea[] = [];
  customers: Customer[] = [];
  businessAreaTypes: BusinessAreaType[] = [];
  responseMessage: ResponseMessage | null = null;

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<BusinessAreaFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data: BusinessArea, 
     private businessAreaService: BusinessAreaService,
     private customerService:CustomerService,
     private businessAreaTypeService: BusinessAreaTypeService) {
      }

  ngOnInit() {
    this.form = this.fb.group({
      id: new FormControl(0),
      name: new FormControl(''),
      businessAreaTypeId: new FormControl(0),
      customerId: new FormControl(0),
      createdBy: new FormControl(''),      
      dateCreated: new FormControl(new Date()),
      dateModified: new FormControl(new Date()),
      modifiedBy:new FormControl('')
    });

    this.form.patchValue(this.data);

    this.getBusinessAreaTypes();

    this.getCustomers();
  }

  onSubmit(){
    if(this.form.valid){
      if(this.data){
        this.businessAreaService.updateBusinessArea( this.form.value).subscribe({
          next:(response)  => {
            {
              if(response.success){
                alert('business area type updated.');
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
        this.businessAreaService.addBusinessArea(this.form.value).subscribe((response) =>{
          if(response.success){
            alert('business area type added');
            this.form.reset;
            this.dialogRef.close(true);
          }else{
            alert('business area type not added successfully.')
          }
        })
      }
    }
    else{
      this.responseMessage = {success: false, message:" form is not valid", timeStamp: new Date()}
    }
  }

  getCustomers(){
    this.customerService.getAllCustomers().subscribe((response) =>{
      if(response){
        this.customers = response;
      }
    })
  }

  getBusinessAreaTypes(){
    this.businessAreaTypeService.getAllBusinessAreaTypes().subscribe((response) => {
      if(response){
        this.businessAreaTypes = response;
      }
    })
  }

}
