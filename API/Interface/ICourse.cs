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
        Task<Course> GetById(int id);
        Task DeleteCource(Course obj);
        Task UpdateCourse(int id,CourseDisplayDto obj);
        Task<CourseDisplayDto> GetCourseDisplayDtoById(int id);
        Task<Course>AddCoursewithImage(string User_Id,CourseCreateDto model);
        Task<List<FacultyCourseDto>> GetCourseByFacultyId(string user_id);
    }
}