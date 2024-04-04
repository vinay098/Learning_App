import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SkillModuleDto } from '../interface/skill-module-dto';
import { ShowSkills } from '../interface/show-skills';
import { Module } from '../interface/module';
import { MapperService } from '../service/mapper.service';
import { SkillsService } from '../service/skills.service';
import { Router } from '@angular/router';
import { ModuleService } from '../service/module.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-skill-module',
  templateUrl: './skill-module.component.html',
  styleUrl: './skill-module.component.css'
})
export class SkillModuleComponent {

  smForm: FormGroup = new FormGroup({});
  SkillModuleDto!: SkillModuleDto[];
  Skills!: ShowSkills[];
  module!: Module[];
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};

  constructor(private mapperService: MapperService, private skillService: SkillsService,
    private router: Router, private moduleService: ModuleService,
    private formBuilder: FormBuilder, private toastr: ToastrService) { }

  ngOnInit() {
    this.getSkillModule();
    this.getSkill();
    this.getModule();
    this.smForm = this.formBuilder.group({
      moduleName: [0],
      skillName: [0]
    })

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true,
    };
  }

  getSkill() {
    let tmp = [];
    this.skillService.getSkills().subscribe({
      next: (res) => {
        this.Skills = res;
        for (let i = 0; i < res.length; i++) {
          tmp.push({ id: res[i].id, name: res[i].name });
        }
        this.dropdownList = tmp;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  getModule() {
    this.moduleService.getAllModules().subscribe({
      next: (res) => {
        this.module = res;
      }
    })
  }

  getSkillModule() {
    this.mapperService.getSkillModuleDtovalue().subscribe({
      next: (res) => {
        this.SkillModuleDto = res;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  submit()
  {
    const formvalues = 
    {
      ModuleId : this.smForm.get('moduleName').value,
      SkillId : this.selectedItems.map(x=>x.id)
    }
    // console.log(formvalues);
    this.mapperService.addSkillMod(formvalues).subscribe({
      next:(res)=>
      {
        this.toastr.success("Mapped data Successfully");
        this.selectedItems=[];
        this.smForm.reset();
        this.getSkillModule();
      },
      error:(err)=>
      {
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
    console.log(this.selectedItems);
  }

  deleteMapped(id:number)
  {
    this.mapperService.deleteSkillModule(id).subscribe({
      next:(res)=>{
        this.getSkillModule();
        this.toastr.success("Mapped Data Deleted");
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }
}
