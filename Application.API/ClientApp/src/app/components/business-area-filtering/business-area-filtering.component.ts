import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessAreaFiltering } from 'src/app/models/business-area';
import { BuinsessAreaFilteringService } from 'src/app/services/buinsess-area-filtering.service';

@Component({
  selector: 'app-business-area-filtering',
  templateUrl: './business-area-filtering.component.html',
  styleUrls: ['./business-area-filtering.component.scss']
})


export class BusinessAreaFilteringComponent implements OnInit,AfterViewInit {





  displayedColumns: string[] = ['customerId', 'customerName', 'businessAreaName', 'person'];
  dataSource: MatTableDataSource<BusinessAreaFiltering>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  businessAreafiltering: BusinessAreaFiltering[] = [];
selectedBusinessAreaType!: number ;
options: BusinessAreaFiltering[] = [];
filteredOptions:BusinessAreaFiltering[] = [];
filteredTableData: BusinessAreaFiltering[] = [];
filterValue = '';

  constructor(private businessAreaFilteringService: BuinsessAreaFilteringService) {;

  this.getAllBusinessFilters(1);
  // Assign the data to the data source for the table to render
  this.dataSource = new MatTableDataSource(this.businessAreafiltering);}
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    
  }

  ngOnInit() {
    if(this.businessAreafiltering.length > 0){  
      this.getAllBusinessFilters(1);
  }
  }

  applyFilter(event: Event) {
    /*const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }*/

    this.filteredOptions = this.businessAreafiltering.filter(option => option.createdBy.toLowerCase().includes(this.filterValue.toLowerCase()));

    

  }

  getTableData(id: number){
 this.getAllBusinessFilters(id);
  }

  getAllBusinessFilters(id: number){
    this.businessAreaFilteringService.getAllBusinessAreaFiltering(id).subscribe(
      res => {
        if(res){
          this.businessAreafiltering = res;
          console.log(this.businessAreafiltering);
          return
        }
      }
    )
  }

  

}
