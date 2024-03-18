import { Component, OnInit } from '@angular/core';
import { BatchService } from '../service/batch.service';
import { Batch } from '../interface/batch';
import { SkillsService } from '../service/skills.service';
import { Skill } from '../interface/skill';
import { ShowSkills } from '../interface/show-skills';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { error } from 'console';

@Component({
  selector: 'app-batches',
  templateUrl: './batches.component.html',
  styleUrl: './batches.component.css'
})
export class BatchesComponent implements OnInit {

  batchForm: FormGroup = new FormGroup({});
  batches!: Batch[];
  skills!: ShowSkills[];
  isEdit:boolean = false;
  batchId:number;
  constructor(private batchService: BatchService
    , private skillService: SkillsService, 
    private formBuilder: FormBuilder,
    private toastr:ToastrService,
    private router:Router,
    private route:ActivatedRoute) { }



  ngOnInit(): void {
    
    this.batchId = this.route.snapshot.params["id"];
    if(this.batchId)
    {
      this.isEdit=true;
      this.batchService.getBatchDtoById(this.batchId).subscribe({
        next:(res)=>{
          console.log(res);
          
          this.batchForm.patchValue({
            name:res.name,
            capacity:res.capacity,
            technology:res.technology,
          });
        }
      })
      
    }
    // this.router.navigateByUrl("/batch");
    this.getBatches();
    this.getSkills();

    this.batchForm = this.formBuilder.group({
      name: [null],
      startDate: [null],
      endDate: [null],
      capacity: [null],
      technology: [null],
      skill_Name: [null]
    });
  }

  getBatches() {
    this.batchService.getAllBatches().subscribe({
      next: (res) => {
        // this.router.navigateByUrl("/batch");
        this.batches = res;
        // console.log(res);
      },
      error: (err) => {
        console.log(err);

      }
    })
  }

  getSkills() {
    this.skillService.getSkills().subscribe({
      next: (res) => {
        this.skills = res;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  submit(){
    if(this.isEdit)
    {
      this.batchService.updateBatch(this.batchId,this.batchForm.value).subscribe({
        next:(res)=>{
          this.toastr.success("updated Successfully");
          this.router.navigateByUrl("/home/batch")
          this.batchForm.reset();
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
    else{
      this.batchService.addBatch(this.batchForm.value).subscribe({
        next:(res:Batch)=>{
          this.batches.push(res);
          this.toastr.success("batch added successfully");
          this.router.navigateByUrl("/home/batch");
          this.batchForm.reset();
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
  }

  edit(id:number)
  {
    this.router.navigateByUrl("/home/batch/"+id);
  }

  deleteBatch(id:number)
  {
    this.batchService.deleteBatch(id).subscribe({
      next:(res)=>{
        this.getBatches();
        this.toastr.success("batch deleted successfully");
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }
}
