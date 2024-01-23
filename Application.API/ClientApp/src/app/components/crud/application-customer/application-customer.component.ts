import { ApplicationService } from 'src/app/services/application.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ApplicationCustomers } from 'src/app/models/application-customer';
import { ApplicationCustomerService } from 'src/app/services/crud/application-customer.service';
import { ApplicationCustomerFormComponent } from './application-customer-form/application-customer-form.component';
import { CustomerService } from 'src/app/services/crud/customer.service';
import { Customer } from 'src/app/models/customer';
import { ApplicationCrudService } from 'src/app/services/crud/application-crud.service';
import { Application } from 'src/app/models/application';
import { DialogComponent } from '../../dialog/dialog.component';

@Component({
  selector: 'app-application-customer',
  templateUrl: './application-customer.component.html',
  styleUrls: ['./application-customer.component.scss']
})
export class ApplicationCustomerComponent implements OnInit {

  content: string = 'manage person';
  tableData: ApplicationCustomers[] = [];
  customerName: string = '';
  customer = '';
  applicationName!: string;
  tableColumns!: string[];
  dataSource:MatTableDataSource<ApplicationCustomers> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private applicationCustomerService: ApplicationCustomerService, private dialog: MatDialog,
    private customerService: CustomerService, private applicationService: ApplicationCrudService) { 
    this.tableColumns = ['applicationName','customerName','friendlyUrl', 'createdBy', 'dateCreated', 'action'];
  }


  ngOnInit() {
    this.getAllApplicationCustomers();
  }

  getAllApplicationCustomers(){
    this.applicationCustomerService.getAllApplicationsCustomers().subscribe((response) => {
      if(response){
        this.tableData = response;
        for(var i=0; i < this.tableData.length; i++){
          this.tableData[i].applicationName == this.getApplication(1);
        }

        this.dataSource = new MatTableDataSource<ApplicationCustomers>(this.tableData);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }else{
        alert('something went wrong.')
      }
    })
  }

  openDialog(){
        const dialogRef = this.dialog.open(ApplicationCustomerFormComponent, {width: '1000px'});

        dialogRef.afterClosed().subscribe((val) => {
          if(val){
            this.getAllApplicationCustomers();
          }
        })
  }

  openEditForm(data: ApplicationCustomers ){
    const dialogRef = this.dialog.open(ApplicationCustomerFormComponent, { data});

    dialogRef.afterClosed().subscribe((val) => {
      if(val){
        this.getAllApplicationCustomers();
      }
    })
  }

  deleteApplicationCustomer(data: ApplicationCustomers){
    const deleteDialogRef = this.dialog.open(DialogComponent, {data});
 
    deleteDialogRef.afterClosed().subscribe((value)=>{
     if(value){
       this.applicationCustomerService.deleteApplicationCustomer(data).subscribe((response) =>{
         if(response.success){
           alert(`record was deleted succesfully.`);
           this.getAllApplicationCustomers();
         }else{
           alert("record was not deleted successfully.");
         }
       }
       )
     }
    })
   }

  getCustomer(customerId: number): string{
    
    this.customerService.getAllCustomers().subscribe((response) =>{
      if(response){
        if(response.filter(x => x.id == customerId)[customerId]){
          return response.find(x => x.id == customerId)?.name
        }
      }  
      
      return '';
    });

    return this.customerName;
  }

  getApplication(id: number): string{
    this.applicationService.getApplication(id).subscribe((response) =>{
      if(response){
        this.applicationName = response.name;
      }     
    })
    return this.applicationName;
  }

}
