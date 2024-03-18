import { Component } from '@angular/core';
import { AuthServiceService } from '../../service/auth-service.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  constructor(public authServiec:AuthServiceService){}

  logout(){
    this.authServiec.logout();
  }

}
