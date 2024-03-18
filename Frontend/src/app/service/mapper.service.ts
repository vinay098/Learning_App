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

  addSkillModuleValue(mapType:SkillModule)
  {
    return this.http.post(this.mapperUrl+"skill-module-map",mapType);
  }

  getSkillModuleDtovalue()
  {
    return this.http.get<SkillModuleDto[]>(this.mapperUrl+"get-skill-module");
  }

  addBatchModulevalue(mapType:BatchModule)
  {
    return this.http.post(this.mapperUrl+"batch-module-map",mapType);
  }

  getBatchModuleDtoValue()
  {
    return this.http.get<BatchModuleDisplayDto[]>(this.mapperUrl+"get-batch-module");
  }
}
