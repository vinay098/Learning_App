import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthServiceService } from '../service/auth-service.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from '../service/admin.service';

@Component({
  selector: 'app-add-admin',
  templateUrl: './add-admin.component.html',
  styleUrl: './add-admin.component.css'
})
export class AddAdminComponent {
  registerForm: FormGroup = new FormGroup({});
  submitted = false;
  errorMessages: string[] = [];
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-regular fa-eye-slash";
  formattedNumber: any;

  constructor(private formBuilder: FormBuilder,
    private authService: AuthServiceService,
    private router: Router, private toastr: ToastrService,private adminService:AdminService
  ) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      dob: [null],
      gender: [null],
      phoneNumber: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      userName: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
    })
  }


  register() {
    this.submitted = true;
    this.errorMessages = [];
    if (this.registerForm.valid) {
      this.adminService.addAdmin(this.registerForm.value).subscribe({
        next: (res) => {
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

  hideshowpass() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-regular fa-eye" : this.eyeIcon = "fa-regular fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

}
