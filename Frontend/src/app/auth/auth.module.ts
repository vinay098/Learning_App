import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import {  RouterLink, RouterOutlet } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ValidationMessageComponent } from './validation-message/validation-message.component';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';




@NgModule({
  declarations: [
    LoginComponent,
    SignupComponent,
    ValidationMessageComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterOutlet,
    RouterLink,
    NgxMaskDirective,
    NgxMaskPipe
  ],
  exports:[
    LoginComponent,
    SignupComponent
  ]
})
export class AuthModule { }
