using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CourseCreateDto
    {
        public int id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public int Batch_Id {get;set;}
        public IFormFile Image {get;set;}
        public string image_path {get;set;}
    }
}