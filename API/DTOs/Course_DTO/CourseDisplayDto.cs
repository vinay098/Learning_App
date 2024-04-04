using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Course_DTO
{
    public class CourseDisplayDto
    {
        public void AddSkill(string skillName)
        {
            SkillNames.Add(skillName);
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string BatchName { get; set; }
        public List<string> SkillNames { get; set; } = new List<string>();
        public string imageUrl {get;set;}
    }
}