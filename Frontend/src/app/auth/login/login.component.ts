import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthServiceService } from '../../service/auth-service.service';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { SetUser } from '../../interface/set-user';
import { Auth2Service } from '../../service/auth2.service';
import { ToastrService } from 'ngx-toastr';
import { UserStoreService } from '../../service/user-store.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginform:FormGroup=new FormGroup({});
  submitted=false;
  errorMessages:string[] = [];
  type : string ="password";
  isText : boolean = false;
  eyeIcon : string = "fa-regular fa-eye-slash";

  constructor(private authService:AuthServiceService,
    private formBuilder:FormBuilder,
    private router:Router,private auth2:Auth2Service,
    private toastr: ToastrService,private userStore:UserStoreService){
      //not show Login page if user Logged In

      this.authService.user$.pipe(take(1)).subscribe({
        next:(user:SetUser |null)=>{
          if(user){
           this.router.navigateByUrl('/home');
          }
        }
      })
    }

  ngOnInit(): void {
    this.loginform= this.formBuilder.group({
      userName:['',[Validators.required]],
      password:['',[Validators.required,Validators.minLength(6),Validators.maxLength(15)]]
    });
  }
  
  hideshowpass(){
    this.isText =!this.isText;
    this.isText  ? this.eyeIcon = "fa-regular fa-eye" : this.eyeIcon = "fa-regular fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
 }

  login(){
    this.submitted = true;
    this.errorMessages = [];
    if(this.loginform.valid){
      this.authService.login(this.loginform.value).subscribe({
        next:(res) =>{
          const payload = this.authService.decodedToken();
          console.log(payload);
          console.log(payload.unique_name+" "+payload.role);
          this.userStore.setRoleFromStore(payload.role);
          this.toastr.success("Login Success");
          if(payload.role === 'employee')
          {
            this.router.navigateByUrl('/employee');
          }
          else{
            this.router.navigateByUrl('/home');
          }
        },
        error: error =>{
          console.log(error);
          if(error.error.errors){
            this.errorMessages = error.error.errors;
          }
          else{
            this.errorMessages.push(error.error);
          }
          
        }
      });
    }
   
  }

}
