import { Component, OnInit, numberAttribute } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BatchModuleDisplayDto } from '../interface/batch-module-display-dto';
import { Batch } from '../interface/batch';
import { Module } from '../interface/module';
import { MapperService } from '../service/mapper.service';
import { BatchService } from '../service/batch.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ModuleService } from '../service/module.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { log } from 'console';


@Component({
  selector: 'app-batch-module-map',
  templateUrl: './batch-module-map.component.html',
  styleUrl: './batch-module-map.component.css'
})
export class BatchModuleMapComponent implements OnInit {

  batchModuleForm: FormGroup = new FormGroup({});
  batchModuleDto!: BatchModuleDisplayDto[];
  batch!: Batch[]
  module!: Module[]
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};


  constructor(private mapperService: MapperService, private batchService: BatchService,
    private router: Router, private toastr: ToastrService, private moduleService: ModuleService,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {

    this.getBatch();
    this.getModule();
    this.getBatchModuleMapvalues();

    this.batchModuleForm = this.formBuilder.group({
      batchName: [0],
      moduleName: [0]
    });


    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      // selectAllText: 'Select All',
      // unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      enableCheckAll: false,
    
    };
   
    
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

  getModule(){
    let tmp = [];
    this.moduleService.getAllModules().subscribe({
      next: (res) => {
        this.module = res;
        for (let i = 0; i < res.length; i++) {
          tmp.push({id:res[i].id ,name: res[i].name });
        }
        this.dropdownList = tmp;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  getBatchModuleMapvalues() {
    this.mapperService.getBatchModuleMappedValues().subscribe({
      next: (res) => {
        this.batchModuleDto = res;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  submit() {
    const formData = {
      Batch_Id : this.batchModuleForm.get('batchName').value,
      Module_Id : this.selectedItems.map(x=>x.id)
    }
    this.mapperService.addSkillModule(formData).subscribe({
      next: (res) => {
        this.toastr.success("value Added Successfuly");
        this.selectedItems=[];
        this.batchModuleForm.reset();
        this.getBatchModuleMapvalues();
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }

  onItemSelect(item: any) {
    this.selectedItems.push(item);
  }

  onItemDeSelect(item: any) {
    this.selectedItems = this.selectedItems
    .filter(selectedItem => selectedItem.id !== item.id);
  }


  deleteVal(id:number)
  {
    this.mapperService.deleteBatchModule(id).subscribe({
      next:(res)=>{
        this.getBatchModuleMapvalues();
        this.toastr.success("batch deleted successfully");
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }

}
