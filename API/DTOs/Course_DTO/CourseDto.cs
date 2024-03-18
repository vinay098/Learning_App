using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CourseDto
    {
        public int CourseId {get; set; }
        public string CourseName {get; set; }
        public string CourseDescription {get; set; }
        public string BatchName {get; set; }
        public string ModuleName {get; set; }
    }
}