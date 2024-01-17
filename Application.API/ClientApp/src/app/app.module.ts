import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { LoginComponent } from './components/login/login.component';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { HeaderComponent } from './components/nav/header/header.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import { HomeComponent } from './components/home/home.component';
import { BusinessAreaFilteringComponent } from './components/business-area-filtering/business-area-filtering.component';
import { FooterComponent } from './components/nav/footer/footer.component';
import { MatPaginatorModule} from '@angular/material/paginator';
import { MatSortModule} from '@angular/material/sort';
import { MatTableModule} from '@angular/material/table';
import { CustomersComponent } from './components/customers/customers.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import {MatDividerModule} from '@angular/material/divider';
import {MatListModule} from '@angular/material/list';
import { PersonComponent } from './components/crud/person/person.component';
import { MatDialogModule} from '@angular/material/dialog';
import { PersonFormComponent } from './components/crud/person/person-form/person-form.component';
import { CustomerComponent } from './components/crud/customer/customer.component';
import { CustomerFormComponent } from './components/crud/customer/customer-form/customer-form.component';
import { ApplicationComponent } from './components/crud/application/application.component';
import { ApplicationFormComponent } from './components/crud/application/application-form/application-form.component';
import { BusinessAreaComponent } from './components/crud/business-area/business-area.component';
import { BusinessAreaFormComponent } from './components/crud/business-area/business-area-form/business-area-form.component';
import { ApplicationCustomerComponent } from './components/crud/application-customer/application-customer.component';
import { BusinessAreaRelatioshipComponent } from './components/crud/business-area-relatioship/business-area-relatioship.component';
import { BusinessAreaRelationshipFormComponent } from './components/crud/business-area-relatioship/business-area-relationship-form/business-area-relationship-form.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LandingPageComponent,
    HeaderComponent,
    HomeComponent,
    BusinessAreaFilteringComponent,
    FooterComponent,
    CustomersComponent,
    PageNotFoundComponent,
    PersonComponent,
    PersonFormComponent,
    CustomerComponent,
    CustomerFormComponent,
    ApplicationComponent,
    ApplicationFormComponent,
    BusinessAreaComponent,
    BusinessAreaFormComponent,
    ApplicationCustomerComponent,
    ApplicationFormComponent,
    BusinessAreaRelatioshipComponent,
    BusinessAreaRelationshipFormComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSlideToggleModule,
    FormsModule,
    MatSelectModule,
    MatRadioModule,
    MatCardModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatToolbarModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    MatDividerModule,
    MatListModule,
    MatDialogModule,
    FormsModule,
    MatFormFieldModule,




   /* BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule,
    MatSelectModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,*/






  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
