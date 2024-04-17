import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SkillModule } from '../interface/skill-module';
import { SkillModuleDto } from '../interface/skill-module-dto';
import { BatchModule } from '../interface/batch-module';
import { BatchModuleDisplayDto } from '../interface/batch-module-display-dto';
import { AssignBatch } from '../interface/assign-batch';

@Injectable({
  providedIn: 'root'
})
export class MapperService {

  private mapperUrl = "https://localhost:5100/api/Mapper/";
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

  assignBatch(batch:AssignBatch)
  {
    return this.http.post(this.mapperUrl+"assign-batch-to-faculty",batch);
  }

  getBatchModuleById(id:number)
  {
    return this.http.get<any>(this.mapperUrl+"get-batch-module/"+id)
  }

  updateBatchModule(id:number,value:any)
  {

    return this.http.put(this.mapperUrl+"update-Batch-Module/"+id,value);
  }

  getSKillModuleById(id:number)
  {
    return this.http.get<any>(this.mapperUrl+"get-skill-module/"+id)
  }

  updateSkillModule(id:number,value:any)
  {
    return this.http.put(this.mapperUrl+"update-skill-Module/"+id,value);
  }

  buyBatch(val:any)
  {
    const obj = {
      batchId:val
    }
    return this.http.post(this.mapperUrl+"buy-batch",obj);
  }
}
