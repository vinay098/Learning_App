using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CourseSkills
    {
        public int CourseId { get; set; }
        public Course Course {get;set;}
        public int SkillId {get; set; }
        public Skills Skills {get; set; }
    }
}