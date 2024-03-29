import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CoreModule } from '../core/core.module';
import { HomeRoutingModule } from './home-routing.module';
import { SharedModule } from '../shared/shared.module';
import { PrivateComponent } from './components/private/private.component';



@NgModule({
  declarations: [
    DashboardComponent,
    PrivateComponent
  ],
  imports: [
    CommonModule,

    HomeRoutingModule,
    CoreModule,
    SharedModule
  ]
})
export class HomeModule { }
