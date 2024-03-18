import { Component, OnInit } from '@angular/core';
import { MapperService } from '../service/mapper.service';
import { Router } from '@angular/router';
import { SkillModuleDto } from '../interface/skill-module-dto';
import { SkillsService } from '../service/skills.service';
import { ModuleService } from '../service/module.service';
import { ShowSkills } from '../interface/show-skills';
import { Module } from '../interface/module';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-skill-module-map',
  templateUrl: './skill-module-map.component.html',
  styleUrl: './skill-module-map.component.css'
})
export class SkillModuleMapComponent implements OnInit {

  skillModuleForm: FormGroup = new FormGroup({});
  SkillModuleDto!: SkillModuleDto[];
  Skills!: ShowSkills[];
  module!: Module[];

  constructor(private mapperService: MapperService, private skillService: SkillsService,
    private router: Router, private moduleService: ModuleService,
    private formBuilder: FormBuilder, private toastr: ToastrService) { }

  ngOnInit(): void {
    
    this.getSkillModule();
    this.getSkill();
    this.getModule();
    this.skillModuleForm = this.formBuilder.group({
      skillName: [null],
      moduleName: [null]
    })
  }

  getSkill() {
    this.skillService.getSkills().subscribe({
      next: (res) => {
        this.Skills = res;
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

  submit() {
    this.mapperService.addSkillModuleValue(this.skillModuleForm.value).subscribe({
      next: (res) => {
        this.toastr.success("Added Skill Module");
        this.skillModuleForm.reset();
        this.getSkillModule();
        this.router.navigateByUrl("/home/skill-module");
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

}
