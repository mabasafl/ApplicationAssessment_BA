import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Person } from 'src/app/models/person';
import { PersonService } from 'src/app/services/crud/person.service';
import { PersonFormComponent } from './person-form/person-form.component';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.scss']
})
export class PersonComponent implements OnInit {

  content: string = 'manage person';
  tableData: Person[] = [];
  selectedPerson!: Person ;
  tableColumns!: string[];
  dataSource:MatTableDataSource<Person> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private personService: PersonService, private dialog: MatDialog) { 
    this.tableColumns = ['firstName', 'lastName', 'emailAddress', 'contactNumber', 'createdBy', 'dateCreated', 'action'];
  }

  ngOnInit() {
    this.getAllPersons();
  }

  getAllPersons(){
    this.personService.getAllPersons().subscribe((response) => {
      if(response){
        this.tableData = response;
        
        this.dataSource = new MatTableDataSource<Person>(this.tableData);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
         console.log(this.tableData);
      }else{
        alert('something went wrong.')
      }
    })
  }

  openDialog(){
        const dialogRef = this.dialog.open(PersonFormComponent, {width: '1000px'});

        dialogRef.afterClosed().subscribe((val) => {
          if(val){
            this.getAllPersons();
          }
        })
  }

  openEditForm(data: Person ){
    const dialogRef = this.dialog.open(PersonFormComponent, { data, width: '1000px'});

    dialogRef.afterClosed().subscribe((val) => {
      if(val){
        this.getAllPersons();
      }
    })
  }

  deletePerson(id: number){
    let confirm = window.confirm("Are you sure?")
    if(confirm){
      
    }
  }
    
}
