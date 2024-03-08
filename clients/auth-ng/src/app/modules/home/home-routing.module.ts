import { NgModule, inject } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { PrivateComponent } from './components/private/private.component';
import { UserAuthService } from 'src/app/services/user-auth.service';


const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
  },
  {
    path: 'private',
    component: PrivateComponent,
    canActivate: [() => inject(UserAuthService).isAuthenticated()]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class HomeRoutingModule { }


