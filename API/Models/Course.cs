using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Course
    {
        public int Id {get;set;}
        public String Name {get;set;}
        public String Description {get;set;}
        public string ImageUrl {get;set;}
        public int BatchId {get;set;}
        public Batch Batch {get;set;}
        public ICollection<CourseSkills> CourseSkills {get;set;}
        public string UserId {get;set;}
        public User Users{get;set;}
    }
}