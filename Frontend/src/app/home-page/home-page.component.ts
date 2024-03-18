import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '../service/auth-service.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent implements OnInit{

  constructor(public authService:AuthServiceService){}
  ngOnInit(): void {
  }

 

}
