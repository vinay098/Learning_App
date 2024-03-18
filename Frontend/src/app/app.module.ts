import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './auth/auth.module';
import { HomePageComponent } from './home-page/home-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { IndexComponent } from './index/index.component';
import { NavbarComponent } from './utilityComponents/navbar/navbar.component';
import { FooterComponent } from './utilityComponents/footer/footer.component';
import { SkillsComponent } from './skills/skills.component';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { tokenInterceptor } from './Interceptors/token.interceptor';
import { BatchesComponent } from './batches/batches.component';
import { LearnModuleComponent } from './learn-module/learn-module.component';
import { SkillModuleMapComponent } from './skill-module-map/skill-module-map.component';
import { BatchModuleMapComponent } from './batch-module-map/batch-module-map.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { ViewMembersComponent } from './view-members/view-members.component';
import { EmployeeComponent } from './employee/employee.component';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';



@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    IndexComponent,
    NavbarComponent,
    FooterComponent,
    SkillsComponent,
    BatchesComponent,
    LearnModuleComponent,
    SkillModuleMapComponent,
    BatchModuleMapComponent,
    ProfilePageComponent,
    ViewMembersComponent,
    EmployeeComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    RouterModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    NgxMaskDirective,
    NgxMaskPipe
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(withInterceptors([tokenInterceptor])),
    provideNgxMask(),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
