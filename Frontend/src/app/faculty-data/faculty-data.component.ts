import { Component, OnInit } from '@angular/core';
import { Course } from '../interface/course';
import { CourseService } from '../service/course.service';

@Component({
  selector: 'app-faculty-data',
  templateUrl: './faculty-data.component.html',
  styleUrl: './faculty-data.component.css'
})
export class FacultyDataComponent implements OnInit{

  course: Course[];
  constructor(private courseService:CourseService){}

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


  
}
