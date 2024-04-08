import { Component, OnInit } from '@angular/core';
import { BatchService } from '../service/batch.service';
import { ToastrService } from 'ngx-toastr';
import { BatchToBuy } from '../interface/batch-to-buy';
import { MapperService } from '../service/mapper.service';

@Component({
  selector: 'app-batch-to-buy',
  templateUrl: './batch-to-buy.component.html',
  styleUrl: './batch-to-buy.component.css'
})
export class BatchToBuyComponent implements OnInit{

  EnrollBatch:BatchToBuy[];
  constructor(private batchService:BatchService,private toastr:ToastrService
    ,private mapperService:MapperService){}

  ngOnInit(): void {
    this.BatchToENroll();
  }

  BatchToENroll()
  {
    this.batchService.BatchToBuy().subscribe({
      next:(res)=>
      {
        this.EnrollBatch = res;
      }
    })
  }

  buyBatch(id:any)
  {
    this.mapperService.buyBatch(id).subscribe({
      next:(res)=>
      {
        this.toastr.success("You have been enrolled in this batch");
        this.BatchToENroll();
      }
    })
  }


}
