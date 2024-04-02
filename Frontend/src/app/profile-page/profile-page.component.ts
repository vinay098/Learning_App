import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '../service/auth-service.service';
import { SetUser } from '../interface/set-user';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.css'
})
export class ProfilePageComponent implements OnInit {
  User:SetUser
  constructor(private authServiec:AuthServiceService){}

  ngOnInit(): void {
    this.authServiec.user$.subscribe({
      next:(res:SetUser)=>{
        this.User = res
      }
    })
    console.log(this.User);
    
  }
}
