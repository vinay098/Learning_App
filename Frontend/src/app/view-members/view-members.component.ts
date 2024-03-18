import { Component, OnInit } from '@angular/core';
import { AdminService } from '../service/admin.service';
import { ViewMembers } from '../interface/view-members';
import { log } from 'console';

@Component({
  selector: 'app-view-members',
  templateUrl: './view-members.component.html',
  styleUrl: './view-members.component.css'
})
export class ViewMembersComponent implements OnInit {

  members!:ViewMembers[];
  roles:string;

  constructor(private adminService:AdminService){}

  ngOnInit(): void {
    this.getMembers();
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

}
