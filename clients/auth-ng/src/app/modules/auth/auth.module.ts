import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


import { RegisterComponent } from './components/register/register.component';
import { AuthRoutingModule } from './auth-routing.module';
import { SharedModule } from '../shared/shared.module';




@NgModule({
  declarations: [
    RegisterComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,

     //Needed for links inside login/register components
     AuthRoutingModule,
     SharedModule
  ],
  exports: [
    RegisterComponent
  ]
})
export class AuthModule { }
