import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { User } from '../interface/user';
import { Login } from '../interface/login';
import { SetUser } from '../interface/set-user';
import { BehaviorSubject, ReplaySubject, map, of } from 'rxjs';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { isPlatformBrowser } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  User!: SetUser;
  private AuthUrl = "http://localhost:5100/api/Account/";
  private userSource = new BehaviorSubject<SetUser>(null);
  user$ = this.userSource.asObservable();
  storage: Storage;
  uesrPayload: any;
  name: string;
  role: string;

  constructor(private http: HttpClient,
    private router: Router, @Inject(PLATFORM_ID) private platform_id: object,
    private toastr: ToastrService) {
    // this.uesrPayload = this.decodedToken();
    if (isPlatformBrowser(this.platform_id)) {
      this.uesrPayload = this.decodedToken();
      console.log(this.isTokenExpired());
    }
    

  }

  refreshUser(jwt: string | null) {
    if (jwt === null) {
      this.userSource.next(null);
      return of(undefined);
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt)
    return this.http.get<SetUser>(this.AuthUrl + "refresh-token", { headers }).pipe(
      map((user: SetUser) => {
        if (user) {
          this.setUser(user);
        }
      })
    )
  }

  signUp(signup: User) {
    return this.http.post(this.AuthUrl + "register", signup);
  }

  login(login: Login) {
    return this.http.post<SetUser>(this.AuthUrl + "login", login).pipe(
      map((user: SetUser) => {
        if (user) {
          this.setUser(user);
          console.log(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem("appuser");
    this.userSource.next(null);
    this.router.navigateByUrl('/');
  }


  isTokenExpired(): boolean {
    const decoded = this.decodedToken();
    if (!decoded) {
      return true; 
    }
    // console.log(decoded.exp * 1000);
    // console.log(Date.now());
    return decoded.exp * 1000 < Date.now();
  }

  getJwt() {
    const key = localStorage.getItem("appuser");
    if (key) {
      const user: SetUser = JSON.parse(key);
      return user.jwt
    }
    return null;
  }

  private setUser(user: SetUser) {

    localStorage.setItem("appuser", JSON.stringify(user));

    this.userSource.next(user);
    // console.log(user);
  }

  autoLogin() {
    const user = JSON.parse(localStorage.getItem("appuser"));
    if (!user) {
      return;
    }
    const loggedUser = user;
    // console.log(loggedUser);

    if (loggedUser.jwt) {
      this.userSource.next(loggedUser);
    }
  }

  decodedToken() {
    const jwtHelper = new JwtHelperService();
    const token = this.getJwt();
    console.log(jwtHelper.decodeToken(token));
   
    
    
    return jwtHelper.decodeToken(token);
  }

  updateProfile(update:any)
  {
    return this.http.put<SetUser>(this.AuthUrl+"update-profile",update).pipe(
      map((user:SetUser)=>{
        this.setUser(user);
        console.log(user);
      })
    );
    
  }

  
  getRoleFromToken(){
    if(this.uesrPayload){
      return this.uesrPayload.role;
    }

  }


}
