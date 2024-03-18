import { Component } from '@angular/core';
import { AuthServiceService } from '../service/auth-service.service';

@Component({
  selector: 'app-profile-page',
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.css'
})
export class ProfilePageComponent {
  constructor(public authServiec:AuthServiceService){}
}
