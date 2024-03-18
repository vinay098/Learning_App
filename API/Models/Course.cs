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
        public string Batch_Id {get;set;}
        public Batch Batch {get;set;}
        public int ModuleId {get;set;}
        public LearnModule Module {get;set;}
        public ICollection<CourseSkills> CourseSkills {get;set;}
    }
}