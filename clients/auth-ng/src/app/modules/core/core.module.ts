import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/navbar/navbar.component';
import { AuthRoutingModule } from '../auth/auth-routing.module';
import { AuthModule } from '../auth/auth.module';



@NgModule({
  declarations: [
    NavbarComponent
  ],
  imports: [
    CommonModule,

    AuthRoutingModule,
    AuthModule
  ],
  exports: [
    NavbarComponent
  ]
})
export class CoreModule { }
