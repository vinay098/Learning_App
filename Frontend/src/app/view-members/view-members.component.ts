import { Component, OnInit } from '@angular/core';
import { AdminService } from '../service/admin.service';
import { ViewMembers } from '../interface/view-members';
import { log } from 'console';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import e from 'express';

@Component({
  selector: 'app-view-members',
  templateUrl: './view-members.component.html',
  styleUrl: './view-members.component.css'
})
export class ViewMembersComponent implements OnInit {

  members!:ViewMembers[];
  roles:string;
  isEmployee:boolean=false;
  isFaculty:boolean=false;

  constructor(private adminService:AdminService,private toastr:ToastrService,
    private router:Router){}

  ngOnInit(): void {
    this.getMembers();
    if(this.isEmployee)
    {
      this.getEmployee();
    }
    if(this.isFaculty)
    {
      this.getFaculty();
    }
    console.log(this.getMembers());
    
  }

  getMembers()
  {
    this.adminService.getAllMembers().subscribe({
      next:(res)=>{
        this.members= res;
        // console.log(this.members);
        // var isapproved=null;  
        // for(let i=0;i<res.length;i++)
        // {
        //   isapproved = res[i].isApproved;
        //   console.log(isapproved);
          
        // }
      }
    })
  }

  getFaculty()
  {
    this.isFaculty = true;
    this.adminService.getAllFaculty().subscribe({
      next:(res)=>{
        this.members = res;
      }
    })
  }

  getEmployee()
  {
    this.isEmployee = true;
    this.adminService.getAllEmployee().subscribe({
      next:(res)=>{
        this.members= res;
      }
    })
  }

  approve(id:string)
  {
    const memberToApprove = this.members.find(member => member.userId === id);
    if(memberToApprove && !memberToApprove.isApproved )
    {
      this.adminService.approveUser(id).subscribe({
        next:(res)=>{
        
        //  var ans = this.members.find(id).userId
          this.toastr.success("user is been approved");
          this.getMembers();
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
    else{
      this.toastr.info("member is already approved");
    }
   
  }

  disapprove(id:string)
  {
    const memberToApprove = this.members.find(member => member.userId === id);
    if(memberToApprove && memberToApprove.isApproved)
    {
      this.adminService.disApproveUser(id).subscribe({
        next:(res)=>
        {
          // console.log(res);
          this.toastr.warning("user is been disapproved");
          this.getMembers();
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
    else{
      this.toastr.info("user is already not approved");
    }
    
  }

}
