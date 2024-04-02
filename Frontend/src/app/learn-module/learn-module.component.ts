import { Component, OnInit } from '@angular/core';
import { ModuleService } from '../service/module.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Module } from '../interface/module';
import { error } from 'console';


@Component({
  selector: 'app-learn-module',
  templateUrl: './learn-module.component.html',
  styleUrl: './learn-module.component.css'
})
export class LearnModuleComponent implements OnInit {

  moduleForm:FormGroup = new FormGroup({});
  module:Module[];
  isEdit:boolean = false;
  moduleId:number;
  name:any;
  p:number=1;

  constructor(private moduleService:ModuleService,
    private router:Router,private toastr:ToastrService,
    private formBuilder:FormBuilder,private route:ActivatedRoute
    ){}
    
  ngOnInit(): void {
    this.moduleId = this.route.snapshot.params['id'];
    if(this.moduleId)
    {
      this.isEdit=true;
      this.moduleService.getModuleDtoById(this.moduleId).subscribe({
        next:(res)=>{
          this.moduleForm.patchValue(res);
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
    this.getModules();
    this.moduleForm = this.formBuilder.group({
      name:[null],
      level:[null],
      learning_Type: [null],
      certification_Type: [null]
    });
  }

  getModules()
  {
    this.moduleService.getAllModules().subscribe({
      next:(res)=>{
        this.module =res;
      },
      error:(err)=>{
        console.log(err);
        
      }
    })
  }

  submit()
  {
    if(this.isEdit)
    {
      this.moduleService.updateModule(this.moduleId,this.moduleForm.value).subscribe({
        next:(res)=>{
          this.toastr.success("updated Successfully");
          this.router.navigateByUrl("/home/module");
          this.moduleForm.reset()
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
    else{
      this.moduleService.addModule(this.moduleForm.value).subscribe({
        next:(res:Module)=>{
          this.module.push(res);
          this.toastr.success("Module Added SuccessFully");
          this.router.navigateByUrl("/home/module");
          this.moduleForm.reset();
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
  }

  edit(id:number)
  {
    this.router.navigateByUrl("/home/module/"+id);
  }

  deleteModule(id:number)
  { 
    this.moduleService.deleteModule(id).subscribe({
      next:(res)=>{
        this.getModules();
        this.toastr.success("module deleted successfully");
      },
      error:(err)=>
      {
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
      this.module = this.module.filter((res)=>{
        return res.name.toLocaleLowerCase().match(this.name.toLocaleLowerCase());
      })
    }
  }

  private isAscending = true;
  SortAscending()
  {
    this.module = this.module.sort((a:any, b:any) => a.id - b.id);
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
    this.module = this.module.sort((a:any, b:any) => b.id - a.id);
  }

  resetPage()
  {
    this.router.navigateByUrl("/home/module");
  }
}
