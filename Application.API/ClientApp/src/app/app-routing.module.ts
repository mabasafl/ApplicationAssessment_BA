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
import { BusinessAreaRelatioshipComponent } from './components/crud/business-area-relatioship/business-area-relatioship.component';
import { AuthGuard } from './gaurds/auth.guard';

const routes: Routes = [
  {path:'', component: LandingPageComponent},
  {path:':friendlyUrl/login', component:LoginComponent, pathMatch:'full'},
  {path: ':friendlyUrl/app', component: CustomersComponent, canActivate: [AuthGuard]},
  { path: '404',component: PageNotFoundComponent},
    {path: 'crud', component: HomeComponent, canActivate: [AuthGuard]},
    {path: 'crud/person', component: PersonComponent, canActivate: [AuthGuard]},
    {path: 'crud/customer', component: CustomerComponent, canActivate: [AuthGuard]},
    {path: 'crud/application', component: ApplicationComponent, canActivate: [AuthGuard]},
    {path: 'crud/business-area', component: BusinessAreaComponent, canActivate: [AuthGuard]},
    {path: 'crud/application-customer', component: ApplicationCustomerComponent, canActivate: [AuthGuard]},
    {path: 'crud/business-area-relationship', component: BusinessAreaRelatioshipComponent, canActivate: [AuthGuard]},
  {path: '**', redirectTo:'/404'},
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
