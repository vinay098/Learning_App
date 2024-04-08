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
import { BatchModuleMapComponent } from './batch-module-map/batch-module-map.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { ViewMembersComponent } from './view-members/view-members.component';
import { EmployeeComponent } from './employee/employee.component';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { NgxPaginationModule } from 'ngx-pagination';
import { CourseComponent } from './course/course.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { UpdateProfileComponent } from './update-profile/update-profile.component';
import { UpdateRoleComponent } from './update-role/update-role.component';
import { SkillModuleComponent } from './skill-module/skill-module.component';
import { ErrorMessageComponent } from './error-message/error-message.component';
import { AddAdminComponent } from './add-admin/add-admin.component';
import { FacultyDataComponent } from './faculty-data/faculty-data.component';
import { AssignBatchComponent } from './assign-batch/assign-batch.component';
import { BatchToBuyComponent } from './batch-to-buy/batch-to-buy.component';




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
    BatchModuleMapComponent,
    ProfilePageComponent,
    ViewMembersComponent,
    EmployeeComponent,
    CourseComponent,
    UpdateProfileComponent,
    UpdateRoleComponent,
    SkillModuleComponent,
    ErrorMessageComponent,
    AddAdminComponent,
    FacultyDataComponent,
    AssignBatchComponent,
    BatchToBuyComponent,
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
    NgxMaskPipe,
    NgxPaginationModule,
    NgMultiSelectDropDownModule.forRoot(),
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(withInterceptors([tokenInterceptor])),
    provideNgxMask(),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
