
using System.Security.Claims;
using API.DTOs.Skills_DTO;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkills _skillRepo;
        public IHttpContextAccessor _accessor;
        public SkillsController(ISkills skillRepo,
        IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _skillRepo = skillRepo;
        }

        [HttpGet("get-skills"),FormatFilter]
        public async Task<ActionResult> GetAllSkills()
        {
            try
            {
                var skills = await _skillRepo.GetAllSkillsAsync();
                return Ok(skills);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }
        [HttpGet("get-skills.{format}"),FormatFilter]
        public async Task<ActionResult> GetAllSkillsUsingFormatFilter()
        {
            try
            {
                var skills = await _skillRepo.GetAllSkillsAsync();
                return Ok(skills);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpPost("add-skills")]
        public async Task<ActionResult> AddSkills([FromBody] SkillDto skill)
        {
            string user_Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var addedskill = await _skillRepo.AddSkillsAsync(user_Id,skill);
                
                return Ok(addedskill);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("delete-skills/{id}")]
        public async Task<ActionResult> DeleteSkills(int id)
        {
            try
            {
                var GetSkill = await _skillRepo.GetSkillsById(id);
                if (GetSkill == null)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "No SKills Found"
                    });
                }
                await _skillRepo.DeleteSkillsAsync(GetSkill);
                return Ok(GetSkill);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }


        [HttpPut("update-skill/{id}")]
        public async Task<ActionResult> UpdateSkill(int id,SkillDto skill)
        {
            try{
                await _skillRepo.UpdateSkillsAsync(id,skill);
                return Ok(skill);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("get-skill/{id}")]
        public async Task<ActionResult> GetSkill(int id)
        {
            try
            {
                var skill = await _skillRepo.GetSkillDtoById(id);
                if(skill == null)
                {
                    return NotFound("id not found");
                }
                return Ok(skill);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }


    }
}