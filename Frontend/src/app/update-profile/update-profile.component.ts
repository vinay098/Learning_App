import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '../service/auth-service.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SetUser } from '../interface/set-user';

@Component({
  selector: 'app-update-profile',
  templateUrl: './update-profile.component.html',
  styleUrl: './update-profile.component.css'
})
export class UpdateProfileComponent implements OnInit {
  profileForm:FormGroup= new FormGroup({});
  User :SetUser;
  constructor(private authServiec:AuthServiceService,private formBuilder:FormBuilder,
    private router:Router){}

  ngOnInit(): void {
    
    this.authServiec.user$.subscribe({
      next:(res:SetUser)=>{
        this.User = res;
      }
    })
    // console.log(this.User);
    
    this.profileForm = this.formBuilder.group({
      firstName:[this.User.firstName] ,
      lastName: [this.User.lastName],
      phoneNumber:[this.User.phoneNumber] ,
      userName: [this.User.userName,[Validators.required,Validators.minLength(5),Validators.maxLength(20)]],
      email: [this.User.email]
    })
  }

  submit()
  {
    this.authServiec.updateProfile(this.profileForm.value).subscribe({
      next:(res)=>{
        console.log(res);
        this.router.navigateByUrl("/");
      
      },
      error:(err)=>{
        console.log(err);
      }
    })
  }
}
