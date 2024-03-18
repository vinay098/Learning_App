import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BatchModuleDisplayDto } from '../interface/batch-module-display-dto';
import { Batch } from '../interface/batch';
import { Module } from '../interface/module';
import { MapperService } from '../service/mapper.service';
import { BatchService } from '../service/batch.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ModuleService } from '../service/module.service';

@Component({
  selector: 'app-batch-module-map',
  templateUrl: './batch-module-map.component.html',
  styleUrl: './batch-module-map.component.css'
})
export class BatchModuleMapComponent implements OnInit {

  batchModuleForm:FormGroup=new FormGroup({});
  batchModuleDto!:BatchModuleDisplayDto[];
  batch!:Batch[]
  module!:Module[]

  constructor(private mapperService:MapperService,private batchService:BatchService,
    private router:Router,private toastr:ToastrService,private moduleService:ModuleService,
    private formBuilder:FormBuilder){}

  ngOnInit(): void {
    this.getBatch();
    this.getModule();
    this.getBatchModuleMapvalues();
    this.batchModuleForm = this.formBuilder.group({
      batchName:[null],
      moduleName:[null]
    });
  }

  getBatch()
  {
    this.batchService.getAllBatches().subscribe({
      next:(res)=>{
        this.batch =res;
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }

  getModule()
  {
    this.moduleService.getAllModules().subscribe({
      next:(res)=>{
        this.module = res;
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }

  getBatchModuleMapvalues()
  {
    this.mapperService.getBatchModuleDtoValue().subscribe({
      next:(res)=>{
        this.batchModuleDto =res;
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }

  submit()
  {
    this.mapperService.addBatchModulevalue(this.batchModuleForm.value).subscribe({
      next:(res)=>{
        this.toastr.success("value Added Successfuly");
        this.batchModuleForm.reset();
        this.getBatchModuleMapvalues();
      }
    })
  }


}
