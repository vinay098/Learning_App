import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {Module} from '../interface/module';

@Injectable({
  providedIn: 'root'
})
export class ModuleService {

  private moduleUrl = "http://localhost:5100/api/Module/";
  constructor(private http:HttpClient) { }

  addModule(module:Module)
  {
    return this.http.post(this.moduleUrl+"add-module",module);
  }
  getAllModules()
  {
    return this.http.get<Module[]>(this.moduleUrl+"get-all");
  }
  getModuleDtoById(id:number)
  {
    return this.http.get<Module>(this.moduleUrl+"get-module/"+id);
  }
  updateModule(id:number,module:Module)
  {
    return this.http.put<Module>(this.moduleUrl+"update-module/"+id,module);
  }
  deleteModule(id:number)
  {
    return this.http.delete(this.moduleUrl+"delete-module/"+id);
  }
}
