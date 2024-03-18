import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './auth/signup/signup.component';
import { LoginComponent } from './auth/login/login.component';
import { HomePageComponent } from './home-page/home-page.component';
import { authGuard } from './auth/Gaurds/auth.guard';
import { SkillsComponent } from './skills/skills.component';
import { BatchesComponent } from './batches/batches.component';
import { LearnModuleComponent } from './learn-module/learn-module.component';
import { SkillModuleMapComponent } from './skill-module-map/skill-module-map.component';
import { BatchModuleMapComponent } from './batch-module-map/batch-module-map.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { ViewMembersComponent } from './view-members/view-members.component';
import { EmployeeComponent } from './employee/employee.component';




const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home', component: HomePageComponent, canActivate: [authGuard], children: [
      { path: 'skills', component: SkillsComponent },
      { path: 'skills/:id', component: SkillsComponent },
      { path: 'batch', component: BatchesComponent },
      { path: 'batch/:id', component: BatchesComponent },
      { path: 'module', component: LearnModuleComponent },
      { path: 'module/:id', component: LearnModuleComponent },
      { path: 'skill-module', component: SkillModuleMapComponent },
      { path: "batch-module", component: BatchModuleMapComponent },
      { path: '', component: ProfilePageComponent }
    ]
  },
  { path: 'signup', component: SignupComponent },
  { path: 'login', component: LoginComponent },
  {path:'get-members',component:ViewMembersComponent},
  {path:'employee',component:EmployeeComponent,canActivate:[authGuard]}




];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
