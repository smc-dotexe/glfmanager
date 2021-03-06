import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ClientsComponent } from './clients/clients.component';
import { JobsComponent } from './jobs/jobs.component';
import { EmployeesComponent } from './employees/employees.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'clients', component: ClientsComponent },
  { path: 'jobs', component: JobsComponent },
  { path: 'employees', component: EmployeesComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
