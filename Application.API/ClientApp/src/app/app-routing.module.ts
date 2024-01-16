import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { CustomersComponent } from './components/customers/customers.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { HomeComponent } from './components/home/home.component';
import { PersonComponent } from './components/crud/person/person.component';
import { CustomerComponent } from './components/crud/customer/customer.component';
import { ApplicationComponent } from './components/crud/application/application.component';
import { BusinessAreaComponent } from './components/crud/business-area/business-area.component';
import { ApplicationCustomerComponent } from './components/crud/application-customer/application-customer.component';

const routes: Routes = [
  //{path:'url/:friendlyUrl', component: LandingPageComponent},
  {path:':friendlyUrl/login', component:LoginComponent},
  {path: ':friendlyUrl/app', component: CustomersComponent},
  { path: '404',component: PageNotFoundComponent},
  {path: 'crud', component: HomeComponent},
  {path: 'crud/person', component: PersonComponent},
  {path: 'crud/customer', component: CustomerComponent},
  {path: 'crud/application', component: ApplicationComponent},
  {path: 'crud/business-area', component: BusinessAreaComponent},
  {path: 'crud/application-customer', component: ApplicationCustomerComponent},
  {path: '**', redirectTo:'/404'},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
