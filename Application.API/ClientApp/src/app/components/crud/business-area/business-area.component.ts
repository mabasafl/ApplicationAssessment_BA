import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessArea } from 'src/app/models/business-area';
import { BusinessAreaService } from 'src/app/services/crud/business-area.service';
import { BusinessAreaFormComponent } from './business-area-form/business-area-form.component';
import { DialogComponent } from '../../dialog/dialog.component';

@Component({
  selector: 'app-business-area',
  templateUrl: './business-area.component.html',
  styleUrls: ['./business-area.component.scss']
})
export class BusinessAreaComponent implements OnInit {

  content: string = 'manage person';
  tableData: BusinessArea[] = [];
  tableColumns!: string[];
  dataSource:MatTableDataSource<BusinessArea> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private businessAreaService: BusinessAreaService, private dialog: MatDialog) { 
    this.tableColumns = ['name', 'createdBy', 'dateCreated', 'action'];
  }

  ngOnInit() {
    this.getAllBusinessAreas();
  }

  getAllBusinessAreas(){
    this.businessAreaService.getAllBusinessAreas().subscribe((response) => {
      if(response){
        this.tableData = response;
        
        this.dataSource = new MatTableDataSource<BusinessArea>(this.tableData);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
         console.log(this.tableData);
      }else{
        alert('something went wrong.')
      }
    })
  }

  openDialog(){
        const dialogRef = this.dialog.open(BusinessAreaFormComponent, {width: '1000px'});

        dialogRef.afterClosed().subscribe((val) => {
          if(val){
            this.getAllBusinessAreas();
          }
        })
  }

  openEditForm(data: BusinessArea ){
    const dialogRef = this.dialog.open(BusinessAreaFormComponent, { data});

    dialogRef.afterClosed().subscribe((val) => {
      if(val){
        this.getAllBusinessAreas();
      }
    })
  }

  deleteBusinessArea(data: BusinessArea){
    const deleteDialogRef = this.dialog.open(DialogComponent, {data});
 
    deleteDialogRef.afterClosed().subscribe((value)=>{
     if(value){
       this.businessAreaService.deleteBusinessArea(data).subscribe((response) =>{
         if(response.success){
           alert(`record was deleted succesfully.`);
           this.getAllBusinessAreas();
         }else{
           alert("record was not deleted successfully.");
         }
       }
       )
     }
    })
   }

}
