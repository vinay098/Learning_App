import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { AuthServiceService } from './service/auth-service.service';
import {isPlatformBrowser} from '@angular/common';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  sideNavStatus:boolean=false;
  constructor(private authService:AuthServiceService
    ,@Inject(PLATFORM_ID) private platform_id:object,
    ){
    
  }

  ngOnInit(): void {
    if(isPlatformBrowser(this.platform_id))
    {
      this.authService.autoLogin();
      this.refreshToken();
      if(this.authService.isTokenExpired())
      {
        this.authService.logout();
      }
    }
  }

  private refreshToken(){
    const jwt = this.authService.getJwt();
    if(jwt){
      // console.log(jwt);
      this.authService.refreshUser(jwt).subscribe({
        next:_ =>{},
        error: _ =>{
          this.authService.logout();
        }
      })
    }
    else{
      this.authService.refreshUser(null).subscribe();
    }
  }
  
}
