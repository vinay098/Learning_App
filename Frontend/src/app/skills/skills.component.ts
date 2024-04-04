import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SkillsService } from '../service/skills.service';
import { Skill } from '../interface/skill';
import { error, log } from 'console';
import { AuthServiceService } from '../service/auth-service.service';
import { ShowSkills } from '../interface/show-skills';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-skills',
  templateUrl: './skills.component.html',
  styleUrl: './skills.component.css'
})
export class SkillsComponent implements OnInit {

  skillForm:FormGroup = new FormGroup({});
  skills!:ShowSkills[];
  skillId:number;
  isEdit:boolean = false;
  name:any;
  p:number=1;

  constructor(private formBuilder:FormBuilder
    ,private router:Router,
    private skillService:SkillsService,
    private route:ActivatedRoute,
    public authService:AuthServiceService,private toastr:ToastrService){
    }
    
  ngOnInit(): void {
    this.getSkills();
    this.skillId = this.route.snapshot.params["id"]
    if(this.skillId){
      this.isEdit=true;
      this.skillService.getskill(this.skillId).subscribe({
        next:(res)=>{
          // console.log(res);
          this.skillForm.patchValue({
            Name:res.name,
            Family:res.family
          });
        }
      })
    }

   
    this.skillForm = this.formBuilder.group({
      Name: [null],
      Family:[null],
    });
  }

  getSkills(){
    this.skillService.getSkills().subscribe({
      next:(res)=>{
        this.skills = res;
        // console.log(res);
      },
      error:(error)=>{
        console.log(error);
      }
    })
  }

  submit(){
    if(this.isEdit){
      this.skillService.updateSkills(this.skillId,this.skillForm.value).subscribe({
        next:(res)=>{
          // alert("updated");
          this.toastr.success("Updated Successfully");
          this.router.navigateByUrl("/home/skills");
          this.skillForm.reset();
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
    else{
      this.skillService.addSkill(this.skillForm.value).subscribe({
        next:(res:any)=>{
          // alert("Skills added successfully");
          this.toastr.success("Skills added successfully")
          this.skills.push(res);
          this.router.navigateByUrl("/home/skills");
          this.skillForm.reset();
        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
  }

  editSkill(id:number){
    this.router.navigateByUrl('/home/skills/'+id);
  }
  
  deleteskill(id:number){
    this.skillService.deleteSkill(id).subscribe({
      next:(res)=>{
        this.getSkills();
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
      this.skills = this.skills.filter((res)=>{
        return res.name.toLocaleLowerCase().match(this.name.toLocaleLowerCase());
      })
    }
  }

  resetPage()
  {
    this.router.navigateByUrl("/home/skills");
  }

  SortAscending():void 
  {
    this.skills = this.skills.sort((a:any, b:any) => a.id - b.id);
  }
  
  private isAscending = true;
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
    this.skills = this.skills.sort((a:any, b:any) => b.id - a.id);
  }
}
