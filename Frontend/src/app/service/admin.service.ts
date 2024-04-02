import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ViewMembers } from '../interface/view-members';
import { Roles } from '../interface/roles';
import { Admin } from '../interface/admin';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private adminUrl = "http://localhost:5100/api/Admin/";
  constructor(private http:HttpClient) { }

  getAllMembers()
  {
    return this.http.get<ViewMembers[]>(this.adminUrl+"get-members");
  }

  approveUser(id:string)
  {
    return this.http.put("http://localhost:5100/api/Admin/approve-user/"+id,{});
  }

  disApproveUser(id:string)
  {
    return this.http.put(this.adminUrl+"disapprove-user/"+id,{});
  }

  getAllFaculty()
  {
    return this.http.get<ViewMembers[]>(this.adminUrl+"get-faculty");
  }

  getAllEmployee()
  {
    return this.http.get<ViewMembers[]>(this.adminUrl+"get-employee");
  }

  getAllRoles()
  {
    return this.http.get<Roles[]>(this.adminUrl+"get-roles");
  }

  updateRole(id:string,roleName:string)
  {
    const obj = {roleName}
    return this.http.put(this.adminUrl+"update-role/"+id,obj);
  }

  addAdmin(admin:Admin)
  {
    return this.http.post(this.adminUrl+"add-admin",admin);
  }
}
