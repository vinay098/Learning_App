import { Component, OnInit } from '@angular/core';
import { Batch } from '../interface/batch';
import { BatchService } from '../service/batch.service';
import { GetAssignedBatch } from '../interface/get-assigned-batch';
import { AdminService } from '../service/admin.service';
import { ViewMembers } from '../interface/view-members';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import e from 'express';

@Component({
  selector: 'app-assign-batch',
  templateUrl: './assign-batch.component.html',
  styleUrl: './assign-batch.component.css'
})
export class AssignBatchComponent implements OnInit {

 
  members!: ViewMembers[];
  batch!: Batch[];
  assignedBatches!: GetAssignedBatch[];
  assignForm:FormGroup = new FormGroup({});

  constructor(private batchService: BatchService,
    private adminService: AdminService,
    private formBuilder: FormBuilder,private toastr:ToastrService) { }

  ngOnInit(): void {
    this.getBatch();
    this.getFaculty();
    this.getAssignedBatch();

    this.assignForm = this.formBuilder.group({
      batchID:[],
      faculty_id:[]
    })
  }

  getAssignedBatch() {
    this.batchService.getAssignedBatch().subscribe({
      next: (res) => {
        this.assignedBatches = res;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }


  getBatch() {
    this.batchService.getAllBatches().subscribe({
      next: (res) => {
        this.batch = res;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  getFaculty() {
    this.adminService.getAllFaculty().subscribe({
      next: (res) => {
        this.members = res;
      }
    })
  }

  submit()
  {
    this.batchService.assignBatch(this.assignForm.value).subscribe({
      next:(res)=>{
        this.toastr.success("Batch is assigned to Faculty");
        this.getAssignedBatch();
        this.assignForm.reset();
      },
      
    })
  }

}
