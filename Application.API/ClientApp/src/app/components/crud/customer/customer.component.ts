import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Customer } from 'src/app/models/customer';
import { CustomerService } from 'src/app/services/crud/customer.service';
import { CustomerFormComponent } from './customer-form/customer-form.component';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {

  content: string = 'manage person';
  tableData: Customer[] = [];
  selectedPerson!: Customer ;
  tableColumns!: string[];
  dataSource:MatTableDataSource<Customer> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private customerService: CustomerService, private dialog: MatDialog) { 
    this.tableColumns = ['name', 'createdBy', 'dateCreated', 'action'];
  }


  ngOnInit() {
    this.getAllCustomers();
  }

  getAllCustomers(){
    this.customerService.getAllCustomers().subscribe((response) => {
      if(response){
        this.tableData = response;
        
        this.dataSource = new MatTableDataSource<Customer>(this.tableData);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
         console.log(this.tableData);
      }else{
        alert('something went wrong.')
      }
    })
  }

  openDialog(){
        const dialogRef = this.dialog.open(CustomerFormComponent, {width: '1000px'});

        dialogRef.afterClosed().subscribe((val) => {
          if(val){
            this.getAllCustomers();
          }
        })
  }

  openEditForm(data: Customer ){
    const dialogRef = this.dialog.open(CustomerFormComponent, { data});

    dialogRef.afterClosed().subscribe((val) => {
      if(val){
        this.getAllCustomers();
      }
    })
  }

  deleteCustomer(id: number){
    let confirm = window.confirm("Are you sure?")
    if(confirm){
      
    }
  }


}
