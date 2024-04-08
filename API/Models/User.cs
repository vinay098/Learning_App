using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob {get; set ;} 
        public ICollection<Batch> Batches {get;set;}
        public ICollection<Skills> Skills {get;set;}
        public ICollection<LearnModule> Modules {get;set;}
        public ICollection<Course> Courses {get;set;}
        public ICollection<BatchFaculty> BatchFaculties {get;set;}       
    }
}