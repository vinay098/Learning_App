import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserStoreService {

  private UserName$ = new BehaviorSubject<string>("");
  private role$ = new BehaviorSubject<string>("");
  constructor() { }

  public getRoleFromStore()
  {
    return this.role$.asObservable();
  }

  public setRoleFromStore(role:string)
  {
    this.role$.next(role);
  }

  public getUserNameFromStore()
  {
    return this.UserName$.asObservable();
  }

  public setUserNameFromStore(userName:string)
  {
    this.UserName$.next(userName);
  }

}
