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
  isEdit:boolean = false;
  batchId:number;
  name:any;
  p:number=1;

  constructor(
    private batchService: BatchService, 
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
    this.getBatches();

    this.batchForm = this.formBuilder.group({
      name: [null],
      startDate: [null],
      endDate: [null],
      capacity: [null],
      technology: [null],
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

  search(){
    if(this.name === '')
    {
      this.ngOnInit();
    }
    else{
      this.batches = this.batches.filter((res)=>{
        return res.name.toLocaleLowerCase().match(this.name.toLocaleLowerCase());
      })
    }
  }

  private isAscending = true;
  SortAscending()
  {
    this.batches = this.batches.sort((a:any, b:any) => a.id - b.id);
  }

  toggleSortById() {
    if(this.isAscending)
    {
      this.SortAscending();
      this.isAscending=false;
    }
    else{
      this.SortDescending();
      this.isAscending=true;
    }
  }
  
  SortDescending(){
    this.batches = this.batches.sort((a:any, b:any) => b.id - a.id);
  }

  resetPage()
  {
    this.router.navigateByUrl("/home/batch");
  }
}
