import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Course } from '../interface/course';
import { CourseCreateDto } from '../interface/course-create-dto';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private courseUrl = "http://localhost:5100/api/Course/";
  constructor(private http:HttpClient) { }

  addCourse(course:any)
  {
   
    return this.http.post(this.courseUrl+"add-course-with-image",course);
  }
  getCourse()
  {
    return this.http.get<Course[]>(this.courseUrl);
  }

  deleteCourse(id:number)
  {
    return this.http.delete(this.courseUrl+"delete-course/"+id);
  }

  getCourseById(id)
  {
    return this.http.get<Course>(this.courseUrl+"get-by-id/"+id);
  }

  updateCourse(id:number,course:any)
  {
    this.http.put(this.courseUrl+"update-course-with-image"+id,course);
  }


}
