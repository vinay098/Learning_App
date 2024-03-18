using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Course_DTO;
using API.Models;

namespace API.Interface
{
    public interface ICourse
    {
        Task<Course> AddCourseAsync(CourseDto course);
        Task AddCourseSkillAsync(CourseSkillDto course);
        Task<IList<CourseDisplayDto>> GetAllAsync();
        Task<IList<string>> GetAllSkillName(int course_id);
    }
}