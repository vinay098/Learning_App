import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthServiceService } from '../../service/auth-service.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent implements OnInit {
  registerForm: FormGroup = new FormGroup({});
  submitted = false;
  errorMessages: string[] = [];
  type : string ="password";
  isText : boolean = false;
  eyeIcon : string = "fa-regular fa-eye-slash";
  formattedNumber:any;

  constructor(private formBuilder: FormBuilder,
    private authService: AuthServiceService,
    private router: Router,private toastr:ToastrService
  ) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      dob: [null],
      gender: [null],
      contactNumber: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      userName: [null, [Validators.required,Validators.minLength(5),Validators.maxLength(20)]],
      role: [null, [Validators.required]]
    })
  }


  register() {
    this.submitted = true;
    this.errorMessages = [];
    if (this.registerForm.valid) {
      this.authService.signUp(this.registerForm.value).subscribe({
        next: (res) => {
          // alert('Registred Successfully');
          this.toastr.success("Registred Successfully");
          this.router.navigateByUrl('/login');
        },
        error: error => {
          console.log(error);
          if (error.error.errors) {
            this.errorMessages = error.error.errors;
          }
          else {
            this.errorMessages.push(error.error);
          }
        }
      });
    }

  }

  hideshowpass(){
    this.isText =!this.isText;
    this.isText  ? this.eyeIcon = "fa-regular fa-eye" : this.eyeIcon = "fa-regular fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
 }

}
