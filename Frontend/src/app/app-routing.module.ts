import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './auth/signup/signup.component';
import { LoginComponent } from './auth/login/login.component';
import { HomePageComponent } from './home-page/home-page.component';
import { authGuard } from './auth/Gaurds/auth.guard';
import { SkillsComponent } from './skills/skills.component';
import { BatchesComponent } from './batches/batches.component';
import { LearnModuleComponent } from './learn-module/learn-module.component';
import { BatchModuleMapComponent } from './batch-module-map/batch-module-map.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { ViewMembersComponent } from './view-members/view-members.component';
import { EmployeeComponent } from './employee/employee.component';
import { CourseComponent } from './course/course.component';
import { UpdateProfileComponent } from './update-profile/update-profile.component';
import { UpdateRoleComponent } from './update-role/update-role.component';
import { SkillModuleComponent } from './skill-module/skill-module.component';
import { AddAdminComponent } from './add-admin/add-admin.component';
import { FacultyDataComponent } from './faculty-data/faculty-data.component';
import { AssignBatchComponent } from './assign-batch/assign-batch.component';


const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home', component: HomePageComponent, canActivate: [authGuard], children: [
      { path: '', component: ProfilePageComponent },
      { path: 'update', component: UpdateProfileComponent },
      { path: 'skills', component: SkillsComponent },
      { path: 'skills/:id', component: SkillsComponent },
      { path: 'batch', component: BatchesComponent },
      { path: 'batch/:id', component: BatchesComponent },
      { path: 'module', component: LearnModuleComponent },
      { path: 'module/:id', component: LearnModuleComponent },
      { path: "batch-module", component: BatchModuleMapComponent },
      { path: 'approve-disapprove', component: ViewMembersComponent },
      { path: "update-role", component: UpdateRoleComponent },
      { path: "sm-map", component: SkillModuleComponent },
      { path: "add-admin", component: AddAdminComponent },
      { path: 'course', component: CourseComponent },
      { path: "my-courses", component: FacultyDataComponent },
      { path: 'course/:id', component: CourseComponent },
      { path: 'assign-batch', component: AssignBatchComponent },
    ]
  },
  { path: 'signup', component: SignupComponent },
  { path: 'login', component: LoginComponent },
  { path: 'employee', component: EmployeeComponent, canActivate: [authGuard] },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
