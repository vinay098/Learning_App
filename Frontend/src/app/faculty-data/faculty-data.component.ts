import { Component, OnInit } from '@angular/core';
import { Course } from '../interface/course';
import { CourseService } from '../service/course.service';
import { ToastrService } from 'ngx-toastr';
import { error } from 'console';
import { Router } from '@angular/router';

@Component({
  selector: 'app-faculty-data',
  templateUrl: './faculty-data.component.html',
  styleUrl: './faculty-data.component.css'
})
export class FacultyDataComponent implements OnInit{

  course: Course[];
  constructor(private courseService:CourseService
    ,private toastr:ToastrService,private router:Router){}

  ngOnInit(): void {
    this.getCourses();
  }
  
  getCourses() {
    this.courseService.getCourse().subscribe({
      next: (res) => {
        this.course = res;
        // console.log(this.course);
      },
      error: (err) => {
        console.log(err);
      }
    }
    )
  }

  deleteCourse(id:number)
  {
    this.courseService.deleteCourse(id).subscribe({
      next:(res)=>
      {
        this.toastr.warning("Course Deleted Successfully");
        this.getCourses();
      },
      error:(err)=>
      {
        console.log(err);
      }
    })
  }

  edit(id:number)
  {
    this.router.navigateByUrl("/home/course/"+id);
  }


  
}
