
using System.Security.Claims;
using API.DTOs;
using API.DTOs.Course_DTO;
using API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourse _courseRepo;
        private readonly IHttpContextAccessor _accessor;
        public CourseController(ICourse courseRepo,IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _courseRepo = courseRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetCourseByFaculty()
        {
            string user_id= _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var res = await _courseRepo.GetCourseByFacultyId(user_id);
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        
        [HttpPost("add-course-with-image")]
        public async Task<ActionResult> AddCoursewithImage([FromForm]CourseCreateDto obj)
        {
            string user_id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var res = await _courseRepo.AddCoursewithImage(user_id,obj);
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

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var res = await _courseRepo.GetCourseDisplayDtoById(id);
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }


        [HttpDelete("delete-course/{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                var course = await _courseRepo.GetById(id);
                if(course == null)
                {
                    return NotFound(new
                    {
                        status=404,
                        message = "batch not found"
                    });
                }
                await _courseRepo.DeleteCource(course);
                return Ok(course);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            
        }

        [HttpPut("update-course-with-image/{id}")]
        public async Task<ActionResult> UpdateCourseWithImage(int id,[FromForm]CourseCreateDto obj)
        {
            try
            {
                await _courseRepo.UpdateCourseWithImage(id,obj);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}