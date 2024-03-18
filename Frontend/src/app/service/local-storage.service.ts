import { Injectable } from '@angular/core';
import { AppComponent } from '../app.component';
import { SetUser } from '../interface/set-user';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  private userSource = new BehaviorSubject<SetUser>(null);
  User!:SetUser;
  storage:Storage;
  constructor() {
    // AppComponent.isBrowser.subscribe(isBrowser=>{
    //   if(isBrowser)
    //   {
    //     this.storage = localStorage;
    //   }
    // })
   }

   autoLogin()
  {
    const user = JSON.parse(this.storage.getItem("appuser")) ;
    if(!user)
    {
      return;
    }
    this.User = user;
    const loggedUser = this.User;
    console.log(loggedUser);
    

    if(loggedUser.jwt){
      this.userSource.next(loggedUser);
    }
  }
 
}
