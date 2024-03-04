import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CoreModule } from '../core/core.module';
import { HomeRoutingModule } from './home-routing.module';



@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,

    HomeRoutingModule,
    CoreModule
  ]
})
export class HomeModule { }
