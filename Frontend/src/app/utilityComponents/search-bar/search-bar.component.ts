import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.css'
})
export class SearchBarComponent {

  searchValue:string='';

  @Output()
  searchChange:EventEmitter<string>=new EventEmitter<string>();

  OnSearchTextChange(){
    this.searchChange.emit(this.searchValue);
  }
}
