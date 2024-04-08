import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Batch } from '../interface/batch';
import { AssignBatch } from '../interface/assign-batch';
import { GetAssignedBatch } from '../interface/get-assigned-batch';
import { FacultyData } from '../interface/faculty-data';
import { BatchToBuy } from '../interface/batch-to-buy';

@Injectable({
  providedIn: 'root'
})
export class BatchService {
  private batchUrl = "http://localhost:5100/api/Batch/";
  constructor(private http:HttpClient) { }

  addBatch(batch:Batch)
  {
    return this.http.post(this.batchUrl+"add-batch",batch);
  }
  getAllBatches()
  {
    return this.http.get<Batch[]>(this.batchUrl+"batches");
  }
  getBatchDtoById(id:number)
  {
    return this.http.get<Batch>(this.batchUrl+"batch/"+id);
  }

  updateBatch(id:number,batch:Batch){
    return this.http.put<Batch>(this.batchUrl+"update-batch/"+id,batch);
  }

  deleteBatch(id:number)
  {
    return this.http.delete(this.batchUrl+"delete-batch/"+id);
  }

  getAssignedBatch()
  {
    return this.http.get<GetAssignedBatch[]>(this.batchUrl+"get-assigned-batch");
  }

  getFacultyData()
  {
    return this.http.get<FacultyData[]>(this.batchUrl+"get-data-for-facylty")
  }

  BatchToBuy()
  {
    return this.http.get<BatchToBuy[]>(this.batchUrl+"batch-to-enroll");
  }
}

