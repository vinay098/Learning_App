import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SkillModule } from '../interface/skill-module';
import { SkillModuleDto } from '../interface/skill-module-dto';
import { BatchModule } from '../interface/batch-module';
import { BatchModuleDisplayDto } from '../interface/batch-module-display-dto';

@Injectable({
  providedIn: 'root'
})
export class MapperService {

  private mapperUrl = "http://localhost:5100/api/Mapper/";
  constructor(private http:HttpClient) { }


  getSkillModuleDtovalue()
  {
    return this.http.get<SkillModuleDto[]>(this.mapperUrl+"skill-module-mapped");
  }

  addSkillModule(mapType:any)
  {
    return this.http.post(this.mapperUrl+"add-map",mapType);
  }

  getBatchModuleMappedValues()
  {
    return this.http.get<BatchModuleDisplayDto[]>(this.mapperUrl+"batch-module-mapped-values");
  }

  deleteBatchModule(id:number)
  {
    return this.http.delete(this.mapperUrl+"delete-batch-module/"+id);
  }

  addSkillMod(value:any)
  {
    return this.http.post(this.mapperUrl+"add-skill-module",value);
  }

  deleteSkillModule(id:number)
  {
    return this.http.delete(this.mapperUrl+"delete-skill-module-map/"+id);
  }
}
