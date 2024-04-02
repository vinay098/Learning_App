import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '../service/auth-service.service';
import { UserStoreService } from '../service/user-store.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit{

  role:string='';
  constructor(public authService:AuthServiceService,
    public userStore:UserStoreService){}
  ngOnInit(): void {

    this.userStore.getRoleFromStore()
    .subscribe(val =>{
      console.log(val);
      
      const rolefromtoken = this.authService.getRoleFromToken();
      this.role = val || rolefromtoken;
    })
    console.log(this.role);
  }



 

}
