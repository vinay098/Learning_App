using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class Skills
    {
        [Key]
        public int Id {get; set; }
        public string Skill_Name {get; set; }
        public string Skill_Family {get; set; }
        public string UserId {get; set; }
        public User User {get; set; }
        public ICollection<SkillModule> SkillModules {get; set; }
        public ICollection<CourseSkills> CourseSkills {get;set;}
       
    }
}