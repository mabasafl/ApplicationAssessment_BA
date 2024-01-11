import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BusinessAreaFiltering } from 'src/app/models/business-area-filtering';
import { BusinessAreaType } from 'src/app/models/business-area-type';
import { BuinsessAreaFilteringService } from 'src/app/services/buinsess-area-filtering.service';

@Component({
  selector: 'app-business-area-filtering',
  templateUrl: './business-area-filtering.component.html',
  styleUrls: ['./business-area-filtering.component.scss']
})

export class BusinessAreaFilteringComponent implements OnInit {

  form!: FormGroup;
  categories!: BusinessAreaType[];
  subcategories!: BusinessAreaFiltering[];
  tableData!: BusinessAreaFiltering[];
  tableColumns!: string[];
  dataSource!: MatTableDataSource<BusinessAreaFiltering>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private fb: FormBuilder,
    private businessAreaService: BuinsessAreaFilteringService
  ) {
    this.tableColumns = ['businessAreaName', 'customerName', 'active', 'createdBy', 'dateCreated'];
  }

  ngOnInit() {
    this.form = this.fb.group({
      category: new FormControl(),
      subcategory: new FormControl(),
    });

    this.businessAreaService.getBusinessAreaTypes().subscribe((categories) => {
      this.categories = categories;
    });

    this.form.get('category')?.valueChanges.subscribe((businessAreaId) => {
      if (businessAreaId) {
        this.businessAreaService.getAllBusinessAreaFiltering(businessAreaId).subscribe(
          (subcategories) => {
            this.subcategories = subcategories;
            this.form.get('subcategory')?.setValue(null);
          }
        );
      } else {
        this.subcategories = [];
        this.form.get('subcategory')?.setValue(null);
      }
    });

    this.form.get('subcategory')?.valueChanges.subscribe(
      (subcategoryId) => {
        if (subcategoryId) {
          this.businessAreaService.getData().subscribe(
            (tableData) => {
              this.tableData = tableData;
              this.dataSource = new MatTableDataSource<BusinessAreaFiltering>(this.tableData);
              this.dataSource.paginator = this.paginator;
              this.dataSource.sort = this.sort;
            }
          );
        } else {
          this.tableData = [];
          this.dataSource = new MatTableDataSource<BusinessAreaFiltering>(this.tableData);
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      }
    );
  }
}
