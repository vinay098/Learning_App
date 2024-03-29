import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { AuthServiceService } from './service/auth-service.service';
import {isPlatformBrowser} from '@angular/common';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {


  constructor(private authService:AuthServiceService
    ,@Inject(PLATFORM_ID) private platform_id:object,
    ){
    
  }

  ngOnInit(): void {
    // this.refreshToken();
    // this.authService.autoLogin();
    if(isPlatformBrowser(this.platform_id))
    {
      this.authService.autoLogin();
      this.refreshToken();
    }
  }

  private refreshToken(){
    const jwt = this.authService.getJwt();
    if(jwt){
      console.log(jwt);
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
