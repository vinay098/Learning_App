using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Course_DTO
{
    public class CourseDisplayDto
    {
        public CourseDisplayDto()
        {
            SkillNames = new List<string>();
        }

        public void AddSkill(string skillName)
        {
            SkillNames.Add(skillName);
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string BatchName { get; set; }
        public string ModuleName { get; set; }
        public List<string> SkillNames { get; set; }
    }
}