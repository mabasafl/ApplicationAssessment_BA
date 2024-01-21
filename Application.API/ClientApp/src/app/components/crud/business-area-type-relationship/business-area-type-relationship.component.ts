import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessArea } from 'src/app/models/business-area';
import { BusinessAreaTypeRelationship } from 'src/app/models/business-area-type-relationship';
import { BusinessAreaTypeRelationshipService } from 'src/app/services/business-area-type-relationship.service';
import { BusinessAreaRelationshipService } from 'src/app/services/crud/business-area-relationship.service';
import { BusinessAreaService } from 'src/app/services/crud/business-area.service';
import { BusinessAreaTypeRelationshipFormComponent } from './business-area-type-relationship-form/business-area-type-relationship-form.component';

@Component({
  selector: 'app-business-area-type-relationship',
  templateUrl: './business-area-type-relationship.component.html',
  styleUrls: ['./business-area-type-relationship.component.scss']
})
export class BusinessAreaTypeRelationshipComponent implements OnInit {

  tableData: BusinessAreaTypeRelationship[] = [];
  businessArea!: BusinessArea;
  customerName: string = '';
  customer = '';
  applicationName!: string;
  tableColumns!: string[];
  dataSource:MatTableDataSource<BusinessAreaTypeRelationship> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private businessAreaTypeRelationshipService: BusinessAreaTypeRelationshipService, private dialog: MatDialog,
    ) { 
    this.tableColumns = ['businessAreaId','customerId','businessAreaType1','businessAreaType2','businessAreaType3','isActive', 'createdBy', 'dateCreated', 'action'];
  }


  ngOnInit() {
    this.getAllBusinessAreaTypeRelationships();
  }

  getAllBusinessAreaTypeRelationships(){
    this.businessAreaTypeRelationshipService.getAllBusinessAreaTypeRelationship().subscribe((response) => {
      if(response){
        this.tableData = response;

        this.dataSource = new MatTableDataSource<BusinessAreaTypeRelationship>(this.tableData);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }else{
        alert('something went wrong.')
      }
    })
  }

  openDialog(){
        const dialogRef = this.dialog.open(BusinessAreaTypeRelationshipFormComponent, {width: '1000px'});

        dialogRef.afterClosed().subscribe((val) => {
          if(val){
            this.getAllBusinessAreaTypeRelationships();
          }
        })
  }

  openEditForm(data: BusinessAreaTypeRelationship ){
    const dialogRef = this.dialog.open(BusinessAreaTypeRelationshipFormComponent, { data});

    dialogRef.afterClosed().subscribe((val) => {
      if(val){
        this.getAllBusinessAreaTypeRelationships();
      }
    })
  }

  deleteBusinessAreaRelationship(id: number){
    let confirm = window.confirm("Are you sure?")
    if(confirm){
      
    }
  }
}
