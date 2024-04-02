import { Component } from '@angular/core';
import { ViewMembers } from '../interface/view-members';
import { AdminService } from '../service/admin.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { Roles } from '../interface/roles';

@Component({
  selector: 'app-update-role',
  templateUrl: './update-role.component.html',
  styleUrl: './update-role.component.css'
})
export class UpdateRoleComponent {

  selectedRole:string;
  members!:ViewMembers[];
  roles!:Roles[];
 

  constructor(private adminService:AdminService,private toastr:ToastrService,
    private router:Router){}

  ngOnInit(): void {
    this.getMembers();
    this.getRoles();
    
  }

  getRole(item:any)
  {
    // console.log(item.target);
    this.selectedRole = item.target.value;
    // console.log(this.selectedRole);
    // console.log(typeof(this.selectedRole));
  }

  getMembers()
  {
    this.adminService.getAllMembers().subscribe({
      next:(res)=>{
        this.members= res;
        // console.log(this.members);
      }
    })
  }

  getRoles()
  {
    this.adminService.getAllRoles().subscribe({
      next:(res)=>{
        this.roles = res;
        // console.log(this.roles);
      }
    });
  }

 updateRole(id:string)
 {
    // debugger
    this.adminService.updateRole(id,this.selectedRole).subscribe({
      next:(res)=>{
        this.toastr.success("Role Updated Successfully");
        this.getMembers();
        // this.router.navigateByUrl("/home/update-role")
      },
      error:(err)=>
      {
        // this.toastr.error(err.error.title);
        console.log(err);
      }
    })
 }




}
