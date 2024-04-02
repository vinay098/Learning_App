import { Component } from '@angular/core';
import { AuthServiceService } from '../../service/auth-service.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  constructor(public authServiec:AuthServiceService,private toastr:ToastrService){}

  logout(){
    this.authServiec.logout();
  }

}
