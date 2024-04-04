import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Skill } from '../interface/skill';
import { ShowSkills } from '../interface/show-skills';

@Injectable({
  providedIn: 'root'
})
export class SkillsService {

  private skillUrl = 'http://localhost:5100/api/Skills/';

  constructor(private http:HttpClient) { }

  addSkill(skill:Skill){
    return this.http.post(this.skillUrl+'add-skills',skill);
  }
  getSkills()
  {
    return this.http.get<ShowSkills[]>(this.skillUrl+'get-skills');
  }
  updateSkills(id:number,skill:Skill)
  {
    return this.http.put<Skill>(this.skillUrl+"update-skill/"+id,skill);
  }
  getskill(id:number)
  {
    return this.http.get<ShowSkills>(this.skillUrl+"get-skill/"+id);
  }
  deleteSkill(id:number){
    return this.http.delete(this.skillUrl+"delete-skills/"+id);
  }
}
