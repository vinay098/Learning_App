using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.DTOs;
using API.DTOs.Course_DTO;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class CourseService : ICourse
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course> AddCourseAsync(CourseDto obj)
        {
            var batch_Id = await _context.Batches
            .Where(b => b.BatchName == obj.BatchName)
            .Select(b => b.Id).FirstAsync();

            var module_Id = await _context.Modules
            .Where(m => m.Module_Name == obj.ModuleName)
            .Select(m => m.Id).FirstAsync();

            var add_to_course_model = new Course()
            {
                Name = obj.CourseName,
                Description = obj.CourseDescription,
                Batch_Id = batch_Id.ToString(),
                ModuleId = module_Id,
            };
            _context.Courses.Add(add_to_course_model);
            await _context.SaveChangesAsync();
            return add_to_course_model;
        }

        public async Task AddCourseSkillAsync(CourseSkillDto obj)
        {
            var course_id = await _context.Courses
            .Where(c => c.Name == obj.CourseName)
            .Select(c => c.Id).FirstAsync();

            var skill_id = await _context.Skills
            .Where(s => s.Skill_Name == obj.SkillName)
            .Select(s => s.Id).FirstAsync();

            var addval = new CourseSkills()
            {
                CourseId = course_id,
                SkillId = skill_id
            };

            _context.CourseSkills.Add(addval);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<CourseDisplayDto>> GetAllAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            var get_Batch = _context.Courses.Join(
             _context.Batches,
             c => c.Id,
             b => b.Id,
             (c, b) => new
             {
                 Course_Id = c.Id,
                 Batch_Name = b.BatchName,
             }
            );
            var get_module = _context.Courses.Join(
             _context.Modules,
             c => c.Id,
             m => m.Id,
             (c, m) => new
             {
                 Course_Id = c.Id,
                 Module_Name = m.Module_Name
             }
            );

            // var courses = await _context.Courses.ToListAsync();

            var batch_res = await get_Batch.ToListAsync();
            var module_res = await get_module.ToListAsync();
            var addedData = new List<CourseDisplayDto>();
            var res = new List<string>();

            foreach (var val in courses)
            {
                var dto = new CourseDisplayDto();
                dto.CourseId = val.Id;
                dto.CourseName = val.Name;
                dto.CourseDescription = val.Description;
                foreach (var val3 in batch_res)
                {
                    dto.BatchName = val3.Batch_Name;
                }
                foreach (var val2 in module_res)
                {
                    dto.ModuleName = val2.Module_Name;
                }
                var names = await GetAllSkillName(val.Id);
                foreach (var name in names)
                {
                    dto.AddSkill(name);
                }

                addedData.Add(dto);

            }
            return addedData;
        }



        public async Task<IList<string>> GetAllSkillName(int course_id)
        {
            var ans = await _context.CourseSkills
                  .Where(c => c.CourseId == course_id)
                  .Select(s => s.Skills.Skill_Name).ToListAsync();

            return ans;
        }
    }
}