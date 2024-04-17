import { Component, OnInit } from '@angular/core';
import { BatchService } from '../service/batch.service';
import { ToastrService } from 'ngx-toastr';
import { BatchToBuy } from '../interface/batch-to-buy';
import { MapperService } from '../service/mapper.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-batch-to-buy',
  templateUrl: './batch-to-buy.component.html',
  styleUrl: './batch-to-buy.component.css'
})
export class BatchToBuyComponent implements OnInit {

  list: number[] = [];
  totalCount: number;
  page = 1;
  limit = 3;
  EnrollBatch: BatchToBuy[];
  searchText: string = '';

  constructor(private batchService: BatchService, private toastr: ToastrService
    , private mapperService: MapperService,private route:ActivatedRoute,private router:Router) { 
      
    }

  ngOnInit(): void {
    // this.BatchToENroll();
    this.BatchesPresent();
  }

  onSearchTextEntered(searchValue:string)
  {
    this.searchText=searchValue;
    console.log(this.searchText);
  }

  BatchToENroll() {
    this.batchService.BatchToBuy().subscribe({
      next: (res) => {
        this.EnrollBatch = res;
      }
    })
  }

  buyBatch(id: any) {
    this.mapperService.buyBatch(id).subscribe({
      next: (res) => {
        this.toastr.success("You have been enrolled in this batch");
        this.BatchToENroll();
      }
    })
  }

  BatchesPresent() {
    this.batchService.GetBatchCount().subscribe({
      next: (res) => {
        this.totalCount = res;
        this.list = new Array(Math.ceil(res / this.limit))
        this.batchService.BuyBatch(undefined, undefined, this.page, this.limit).subscribe({
          next: (res) => {
            this.EnrollBatch = res.batches;
            // this.router.navigate(["/home/batch-to-buy",this.page,this.limit])
          }
        })
      }
    })
  }

  getPage(pageNumber: number) {
    this.page = pageNumber;
    this.batchService.BuyBatch(undefined, undefined, this.page, this.limit).subscribe({
      next: (res) => {
        this.EnrollBatch = res.batches;
      }
    });
  }

  getNextPage() {
    if (this.page + 1 > this.list.length) {
      return;
    }
    this.page += 1;
    this.batchService.BuyBatch(undefined, undefined, this.page, this.limit).subscribe({
      next: (res) => {
        this.EnrollBatch = res.batches;
      }
    });
  }

  getPrevPage() {
    if (this.page - 1 < 1) {
      return;
    }
    this.page -= 1;
    this.batchService.BuyBatch(undefined, undefined, this.page, this.limit).subscribe({
      next: (res) => {
        this.EnrollBatch = res.batches;
      }
    });
  }

  onSearch(term: string) {
    this.batchService.BuyBatch(term, undefined, this.page, this.limit).subscribe({
      next: (res) => {
            this.EnrollBatch = res.batches;
      }
    });
  }





}
