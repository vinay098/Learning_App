import { Component, OnInit } from '@angular/core';
import { CourseService } from '../service/course.service';
import { Batch } from '../interface/batch';
import { Module } from '../interface/module';
import { Course } from '../interface/course';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BatchService } from '../service/batch.service';
import { CourseCreateDto } from '../interface/course-create-dto';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrl: './course.component.css'
})
export class CourseComponent implements OnInit {

  batches: Batch[];
  modules: Module[];
  course: Course[];
  selectedImage: File;
  isEdit:boolean=false;
  id:number;
  courseForm: FormGroup = new FormGroup({});
  constructor(private courseService: CourseService,
    private formBuilder: FormBuilder,private toastr:ToastrService,
    private batchService: BatchService,private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.getCourses();
    this.getBatches();
    this.courseForm = this.formBuilder.group({
      name: [null],
      description: [null],
      batch_Id: [null],
      image: [null],
    });
    this.id = this.route.snapshot.params["id"];
    if(this.id)
    {
      this.isEdit = true;
      this.courseService.getCourseById(this.id).subscribe({
        next:(res)=>{
          this.courseForm.patchValue({
            name:res.batchName,
            description:res.courseDescription,
          })
        }
      })
    }
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

  getBatches() {
    this.batchService.getAllBatches().subscribe({
      next: (res) => {
        this.batches = res;
      },
      error: (err) => {
        console.log(err);
      }
    })
  }

  itemChange(event: any) {
    this.selectedImage = event.target.files[0];
  }

  submit() {
    const formData = new FormData();
    formData.append('Name', this.courseForm.value.name);
    formData.append('Description', this.courseForm.value.description);
    formData.append('Batch_Id', this.courseForm.value.batch_Id);
    if (this.selectedImage) {
      formData.append('Image', this.selectedImage);
    }    
    this.courseService.addCourse(formData).subscribe({
      next:(res)=>
      {
        this.toastr.success("Course Added SuccessFully");
        this.getCourses();
        this.courseForm.reset();
      },
      error:(err)=>
      {

        console.log(err);

      }
    })
  }

}
