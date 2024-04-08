import { Component, Input, OnInit } from '@angular/core';


@Component({
  selector: 'app-error-message',
  templateUrl: './error-message.component.html',
  styleUrl: './error-message.component.css'
})
export class ErrorMessageComponent implements OnInit {
  @Input() errorMessages : string[] = [];


  ngOnInit(): void {

  }


}
