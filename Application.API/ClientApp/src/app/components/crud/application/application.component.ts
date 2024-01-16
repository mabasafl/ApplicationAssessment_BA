import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Application } from 'src/app/models/application';
import { ApplicationFormComponent } from './application-form/application-form.component';
import { ApplicationCrudService } from 'src/app/services/crud/application-crud.service';

@Component({
  selector: 'app-application',
  templateUrl: './application.component.html',
  styleUrls: ['./application.component.scss']
})
export class ApplicationComponent implements OnInit {

  content: string = 'manage person';
  tableData: Application[] = [];
  tableColumns!: string[];
  dataSource:MatTableDataSource<Application> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private applicationService: ApplicationCrudService, private dialog: MatDialog) { 
    this.tableColumns = ['name', 'createdBy', 'dateCreated', 'action'];
  }

  ngOnInit() {
    this.getAllApplication();
  }

  getAllApplication(){
    this.applicationService.getAllApplications().subscribe((response) => {
      if(response){
        this.tableData = response;
        
        this.dataSource = new MatTableDataSource<Application>(this.tableData);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
         console.log(this.tableData);
      }else{
        alert('something went wrong.')
      }
    })
  }

  openDialog(){
        const dialogRef = this.dialog.open(ApplicationFormComponent, {width: '1000px'});

        dialogRef.afterClosed().subscribe((val) => {
          if(val){
            this.getAllApplication();
          }
        })
  }

  openEditForm(data: Application ){
    const dialogRef = this.dialog.open(ApplicationFormComponent, { data});

    dialogRef.afterClosed().subscribe((val) => {
      if(val){
        this.getAllApplication();
      }
    })
  }

  deleteApplication(id: number){
    let confirm = window.confirm("Are you sure?")
    if(confirm){
      
    }
  }

}
