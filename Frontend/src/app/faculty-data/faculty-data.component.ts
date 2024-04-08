import { Component, OnInit } from '@angular/core';
import { Course } from '../interface/course';
import { CourseService } from '../service/course.service';
import { ToastrService } from 'ngx-toastr';
import { error } from 'console';
import { Router } from '@angular/router';
import { BatchService } from '../service/batch.service';
import { FacultyData } from '../interface/faculty-data';

@Component({
  selector: 'app-faculty-data',
  templateUrl: './faculty-data.component.html',
  styleUrl: './faculty-data.component.css'
})
export class FacultyDataComponent implements OnInit{

  facultyData: FacultyData[];
  constructor(private courseService:CourseService,private batchService:BatchService
    ,private toastr:ToastrService,private router:Router){}

  ngOnInit(): void {
    this.getFacultyData();
  }
  
  getFacultyData() {
    this.batchService.getFacultyData().subscribe({
      next:(res)=>
      {
        this.facultyData= res;
      }
    })
  }



  
}
