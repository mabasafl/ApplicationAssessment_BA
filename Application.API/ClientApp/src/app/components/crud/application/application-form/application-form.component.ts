import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Application } from 'src/app/models/application';
import { ResponseMessage } from 'src/app/models/response';
import { ApplicationCrudService } from 'src/app/services/crud/application-crud.service';

@Component({
  selector: 'app-application-form',
  templateUrl: './application-form.component.html',
  styleUrls: ['./application-form.component.scss']
})
export class ApplicationFormComponent implements OnInit {

  form!: FormGroup;
  applications: Application[] = [];
  responseMessage: ResponseMessage | null = null;

  constructor(private fb: FormBuilder, private dialogRef: MatDialogRef<ApplicationFormComponent>,
     @Inject(MAT_DIALOG_DATA) public data: Application, 
     private applicationService: ApplicationCrudService) {
      }

  ngOnInit() {
    this.form = this.fb.group({
      id: new FormControl(0),
      name: new FormControl(''),
      createdBy: new FormControl(''),
      dateCreated: new FormControl(new Date()),
      dateModified: new FormControl(new Date()),
      modifiedBy:new FormControl('')
    });

    this.form.patchValue(this.data);
  }

  onSubmit(){
    if(this.form.valid){
      if(this.data){
        this.applicationService.updateApplication( this.form.value).subscribe({
          next:(response)  => {
            {
              if(response.success){
                alert('application updated.');
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
        this.applicationService.addApplication(this.form.value).subscribe((response) =>{
          if(response.success){
            alert('application added');
            this.form.reset;
            this.dialogRef.close(true);
          }else{
            alert('application not added successfully.')
          }
        })
      }
    }
    else{
      this.responseMessage = {success: false, message:" form is not valid", timeStamp: new Date()}
    }
  }

}
