import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthServiceService } from '../service/auth-service.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css'
})
export class ChangePasswordComponent implements OnInit {
  ChangePasswordForm: FormGroup = new FormGroup({});
  type: string = "password";
  typeNew: string = "password";
  typeConfirm: string = "password";
  isText: boolean = false;
  isTextNew: boolean = false;
  isTextConfirm: boolean = false;
  eyeIcon: string = "fa-regular fa-eye-slash";

  constructor(private authService: AuthServiceService, 
    private formBuilder: FormBuilder,private toastr:ToastrService,private router:Router) { }

  ngOnInit(): void {

    this.ChangePasswordForm = this.formBuilder.group({
      OldPassword: [null, [Validators.required]],
      NewPassword: [null, [Validators.required]],
      ConfirmPassword: [null, [Validators.required]]
    })
  }

  submit() {
    this.authService.ChangePassword(this.ChangePasswordForm.value).subscribe({
      next: (res) => {
        this.toastr.success("Password Updated Successfully");
        this.router.navigateByUrl("/");
      },
      error: (err) => {
        console.log(err);
        
      }
    })
  }

  hideshowpass() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-regular fa-eye" : this.eyeIcon = "fa-regular fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  hideshowpassNew() {
    this.isTextNew = !this.isTextNew;
    this.isTextNew ? this.eyeIcon = "fa-regular fa-eye" : this.eyeIcon = "fa-regular fa-eye-slash";
    this.isTextNew ? this.typeNew = "text" : this.typeNew = "password";
  }

  hideshowpassConfirm() {
    this.isTextConfirm = !this.isTextConfirm;
    this.isTextConfirm ? this.eyeIcon = "fa-regular fa-eye" : this.eyeIcon = "fa-regular fa-eye-slash";
    this.isTextConfirm ? this.typeConfirm = "text" : this.typeConfirm = "password";
  }

}
