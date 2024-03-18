import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ViewMembers } from '../interface/view-members';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private adminUrl = "http://localhost:5100/api/Admin/";
  constructor(private http:HttpClient) { }

  getAllMembers()
  {
    return this.http.get<ViewMembers[]>(this.adminUrl+"get-members");
  }
}
