import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessAreaFiltering } from 'src/app/models/business-area-filtering';
import { BusinessAreaRelationshipFormComponent } from './business-area-relationship-form/business-area-relationship-form.component';
import { MatDialog } from '@angular/material/dialog';
import { BusinessAreaRelationshipService } from 'src/app/services/crud/business-area-relationship.service';
import { CustomerService } from 'src/app/services/crud/customer.service';
import { BusinessAreaService } from 'src/app/services/crud/business-area.service';
import { BusinessArea } from 'src/app/models/business-area';

@Component({
  selector: 'app-business-area-relatioship',
  templateUrl: './business-area-relatioship.component.html',
  styleUrls: ['./business-area-relatioship.component.scss']
})
export class BusinessAreaRelatioshipComponent implements OnInit {

  tableData: BusinessAreaFiltering[] = [];
  businessArea!: BusinessArea;
  customerName: string = '';
  customer = '';
  applicationName!: string;
  tableColumns!: string[];
  dataSource:MatTableDataSource<BusinessAreaFiltering> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private businessAreaRelationshipService: BusinessAreaRelationshipService, private dialog: MatDialog,
    private customerService: CustomerService, private businessAreaService:BusinessAreaService) { 
    this.tableColumns = ['businessAreaId','customerName','filteredBusinessAreaId','isActive', 'createdBy', 'dateCreated', 'action'];
  }


  ngOnInit() {
    this.getAllBusinessAreaRelationships();
  }

  getAllBusinessAreaRelationships(){
    this.businessAreaRelationshipService.getAllBusinessAreaRelationships().subscribe((response) => {
      if(response){
        this.tableData = response;

        this.dataSource = new MatTableDataSource<BusinessAreaFiltering>(this.tableData);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }else{
        alert('something went wrong.')
      }
    })
  }

  openDialog(){
        const dialogRef = this.dialog.open(BusinessAreaRelationshipFormComponent, {width: '1000px'});

        dialogRef.afterClosed().subscribe((val) => {
          if(val){
            this.getAllBusinessAreaRelationships();
          }
        })
  }

  openEditForm(data: BusinessAreaFiltering ){
    const dialogRef = this.dialog.open(BusinessAreaRelationshipFormComponent, { data});

    dialogRef.afterClosed().subscribe((val) => {
      if(val){
        this.getAllBusinessAreaRelationships();
      }
    })
  }

  deleteBusinessAreaRelationship(id: number){
    let confirm = window.confirm("Are you sure?")
    if(confirm){
      
    }
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

  getBusinessArea(businessAreaId: number): BusinessArea{
    this.businessAreaService.getBusinessArea(businessAreaId).subscribe((response) => {
      if(response){
        this.businessArea = response;
      }
    })

    return this.businessArea;
  }
}
