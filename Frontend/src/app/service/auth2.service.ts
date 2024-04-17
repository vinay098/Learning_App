import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class Auth2Service {

  private AuthUrl = "https://localhost:5100/api/Account/";
  constructor(private http:HttpClient) { }

  signUp(userobj:any){
    return this.http.post<any>(this.AuthUrl+"register",userobj);
  }

  login(loginobj:any){
    return this.http.post<any>(this.AuthUrl+"login",loginobj);
  }

  storeToken(tokenval:string)
  {
    localStorage.setItem("token",tokenval);
  }
  getToken()
  {
    return localStorage.getItem("token");
  }
  isLoggedIn():boolean
  {
    return !! localStorage.getItem("token");
  }
}
