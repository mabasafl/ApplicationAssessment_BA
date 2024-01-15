import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { CustomersComponent } from './components/customers/customers.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

const routes: Routes = [
  //{path:'url/:friendlyUrl', component: LandingPageComponent},
  {path:':friendlyUrl/login', component:LoginComponent},
  {path: ':friendlyUrl/app', component: CustomersComponent},
  { path: '404',component: PageNotFoundComponent},
  {path: '**', redirectTo:'/404'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
