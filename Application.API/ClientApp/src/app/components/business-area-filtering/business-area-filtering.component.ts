import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { ApplicationCustomers } from 'src/app/models/application-customer';
import { BusinessAreaFiltering } from 'src/app/models/business-area-filtering';
import { BusinessAreaType } from 'src/app/models/business-area-type';
import { CascadeFilter } from 'src/app/models/cascade-filter';
import { Person } from 'src/app/models/person';
import { ApplicationService } from 'src/app/services/application.service';
import { BuinsessAreaFilteringService } from 'src/app/services/buinsess-area-filtering.service';

@Component({
  selector: 'app-business-area-filtering',
  templateUrl: './business-area-filtering.component.html',
  styleUrls: ['./business-area-filtering.component.scss']
})

export class BusinessAreaFilteringComponent implements OnInit {


  //customer stuff
  friendlyUrl!: string;
  applications: ApplicationCustomers[] = [];
  application!: ApplicationCustomers;
  isUrlValid: boolean = false;
  url!: string[];
  currentCustomerId: number = 0;





  @Input() customerInput!: number;
  form!: FormGroup;
  categories!: BusinessAreaFiltering[];
  subcategories!: BusinessAreaFiltering[];
  industries!: BusinessAreaFiltering[];
  tableData!: Person[];
  tableColumns!: string[];
  dataSource!: MatTableDataSource<Person>;

  location!: number;
  department!: number;
  industry!: number;

  dropdownOne!: number;
  dropdownTwo!: number;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private fb: FormBuilder,
    private businessAreaService: BuinsessAreaFilteringService
  ) {
    this.tableColumns = ['firstName', 'lastName', 'emailAddress', 'contactNumber', 'createdBy', 'dateCreated'];
    if(this.customerInput == undefined) this.customerInput = 0
    this.customerInput
  }

  ngOnInit() {
    this.form = this.fb.group({
      location: new FormControl(),
      department: new FormControl(),
      industry: new FormControl(),
    });

    this.location = Number(this.form.get('location')?.value);
    this.department = Number(this.form.get('department')?.value);
    this.industry = Number(this.form.get('industry')?.value);
  
    this.getDropdown(this.location,this.department);

    this.getCascadingFilteringData(this.location,this.department,this.industry);

    
    console.log('data: ', this.getCascadingFilteringData(this.location,this.department,this.industry));

    this.form.get('location')?.valueChanges.subscribe((businessAreaId) => {
      if (businessAreaId) {
        this.dropdownOne = businessAreaId
        this.dropdownTwo = 0;
        this.businessAreaService.getDropDown(this.dropdownOne,this.dropdownTwo,this.customerInput,1).subscribe(
          (subcategories) => {
            this.subcategories = subcategories;
            this.form.get('department')?.setValue(0);
            this.form.get('industry')?.setValue(0);
            this.location = businessAreaId;
            this.department = Number(this.form.get('department')?.value);
            this.industry = Number(this.form.get('industry')?.value);

            this.businessAreaService.getCascadingFilteringData(this.location,this.department,this.industry,this.customerInput,1).subscribe(
              (tableData) => {
                this.tableData = tableData;
                this.dataSource = new MatTableDataSource<Person>(this.tableData);
                this.dataSource.paginator = this.paginator;
                this.dataSource.sort = this.sort;
              }
            );
          }
        );
      } else {
        this.subcategories = [];
        this.form.get('department')?.setValue(0);
      }
    });

    this.form.get('department')?.valueChanges.subscribe(
      (subcategoryId) => {
        if(subcategoryId){
          this.dropdownTwo = subcategoryId;
          this.businessAreaService.getDropDown(this.dropdownOne,this.dropdownTwo,this.customerInput,1).subscribe(
            (subcategories) => {
              this.industries = subcategories;
              this.form.get('industry')?.setValue(0);
              this.department = subcategoryId;
              this.businessAreaService.getCascadingFilteringData(this.location,this.department,this.industry,this.customerInput,1).subscribe(
                (tableData) => {
                  this.tableData = tableData;
                  this.dataSource = new MatTableDataSource<Person>(this.tableData);
                  this.dataSource.paginator = this.paginator;
                  this.dataSource.sort = this.sort;
                }
              );
            })
        }
         else {
          this.tableData = [];
          this.dataSource = new MatTableDataSource<Person>(this.tableData);
          this.form.get('industry')?.setValue(0);
        }
      }
    );
    
    this.form.get('industry')?.valueChanges.subscribe(
      (subcategoryId) => {
        if (subcategoryId) {
          this.industry = subcategoryId
          this.businessAreaService.getCascadingFilteringData(this.location,this.department,this.industry,this.customerInput,1).subscribe(
            (tableData) => {
              this.tableData = tableData;
              this.dataSource = new MatTableDataSource<Person>(this.tableData);
              this.dataSource.paginator = this.paginator;
              this.dataSource.sort = this.sort;
            }
          );
        } else {
          this.tableData = [];
          this.dataSource = new MatTableDataSource<Person>(this.tableData);
        }
      }
    );
  }

  getCascadingFilteringData(ba1: number, ba2: number, ba3: number){
    this.businessAreaService.getCascadingFilteringData(ba1,ba2,ba3,this.customerInput,1).subscribe((data) =>{
      if(data){
        this.tableData = data
        this.dataSource = new MatTableDataSource<Person>(this.tableData);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }
    })
  }

  getDropdown(ba1: number, ba2:number){
    this.businessAreaService.getDropDown(ba1,ba2,this.customerInput,1).subscribe((data) =>{
      if(data){
        this.categories = data;
      }
    })
  }
}
