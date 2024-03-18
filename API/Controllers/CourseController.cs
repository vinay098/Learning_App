using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Course_DTO;
using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourse _courseRepo;

        public CourseController(ICourse courseRepo)
        {
            _courseRepo = courseRepo;
        }

        [HttpPost("add-course")]
        public async Task<ActionResult> AddCourse(CourseDto obj)
        {
            try
            {
                var res = await _courseRepo.AddCourseAsync(obj);
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("map-course-skill")]
        public async Task<ActionResult> MapCourseSkill(CourseSkillDto obj)
        {
            try
            {
                await _courseRepo.AddCourseSkillAsync(obj);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetSkillName()
        {
            try
            {
                 var skills = await _courseRepo.GetAllAsync();
                return Ok(skills);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

    }
}