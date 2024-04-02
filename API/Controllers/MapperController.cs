
using System.Security.Claims;
using API.Context;
using API.DTOs.MapperDto;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapperController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IModule _modulerepo;

        public MapperController(ApplicationDbContext context,
        IHttpContextAccessor accessor,
        IModule modulerepo)
        {
            _context = context;
            _accessor = accessor;
            _modulerepo = modulerepo;
        }

        [HttpPost("add-map")]
        public async Task<ActionResult> MapBM(MapBatchModule obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                foreach (var val in obj.Module_Id)
                {
                    var ob = new BatchModule();
                    ob.BatchId = Convert.ToInt32(obj.Batch_id);
                    ob.ModuleId = val;
                    ob.UserId = user_Id;
                    _context.Add(ob);
                }

                await _context.SaveChangesAsync();
                var res = await _context.BatchModules.ToListAsync();
                return Ok(res);
            }
            catch (Exception e)
            {

                return Problem(e.Message);
            }

        }

        [HttpPost("skill-module-map")]
        public async Task<ActionResult> SkillAndModule(MapSkillmoduleDto obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                foreach (var val in obj.SkillId)
                {
                    var ob = new SkillModule();
                    ob.ModuleId = Convert.ToInt32(obj.ModuleId);
                    ob.SkillId =val;
                    ob.UserId = user_Id;
                    _context.Add(ob);
                }
                await _context.SaveChangesAsync();
                var res = await _context.SkillModules.ToListAsync();
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }


        [HttpGet("batch-module-mapped-values")]
        public async Task<ActionResult<List<BatchModuleDisplayDto>>> BatchModule()
        {
            try
            {
                var batches = await _context.Batches.ToListAsync();
                var addedData = new List<BatchModuleDisplayDto>();
                foreach (var val in batches)
                {
                    var dto = new BatchModuleDisplayDto();
                    dto.BatchId = val.Id;
                    dto.BatchName = val.BatchName;
                    dto.Batch_Start = Convert.ToString(val.StartDate);
                    dto.Batch_End = Convert.ToString(val.EndDate);
                    dto.Capacity = val.Capacity;
                    dto.Technology = val.Technology;
                    var names = await _context.BatchModules
                     .Where(b => b.BatchId == val.Id)
                     .Select(b => b.Module.Module_Name).ToListAsync();
                    foreach (var name in names)
                    {
                        dto.ModuleName.Add(name);
                    }
                    addedData.Add(dto);
                }

                return Ok(addedData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpGet("skill-module-mapped")]
        public async Task<ActionResult<List<SkillModuleValuesDto>>> SkillModule()
        {
            try
            {
                var modules = await _context.Modules.ToListAsync();
                var addedData = new List<SkillModuleValuesDto>();
                foreach(var val in modules)
                {
                    var dto = new SkillModuleValuesDto();
                    dto.ModuleId = val.Id;
                    dto.ModuleName = val.Module_Name;
                    dto.Level = val.Proefficiency_level;
                    dto.Learning_Type = val.Learning_Type;
                    dto.Certification_Type = val.Certification_Type;
                    var names = await _context.SkillModules
                    .Where(m=>m.ModuleId == val.Id)
                    .Select(s=>s.Skills.Skill_Name).ToListAsync();
                    foreach(var name in names)
                    {
                        dto.SkillName.Add(name);
                    }
                    addedData.Add(dto);
                }
                return Ok(addedData);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("delete-batch-module/{id}")]
        public async Task<ActionResult> DeleteMappedValues(int id)
        {
            try
            {
                var res = await _context.BatchModules.Where(b => b.BatchId == id).ToListAsync();
                if (res == null)
                {
                    return NotFound("id not found");
                }
                _context.RemoveRange(res);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("add-skill-module")]
        public async Task<ActionResult> MapSM(MapSkillModule obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                foreach (var val in obj.SkillId)
                {
                    var ob = new SkillModule();
                    ob.ModuleId =Convert.ToInt32(obj.ModuleId);
                    ob.SkillId =val;
                    ob.UserId = user_Id;
                    _context.Add(ob);
                }
                await _context.SaveChangesAsync();
                var res = await _context.SkillModules.ToListAsync();
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("delete-skill-module-map/{id}")]
        public async Task<ActionResult> DeleteSkillModuleMap(int id)
        {
            try
            {
                var res = await _context.SkillModules.Where(m=>m.ModuleId==id).ToListAsync();
                if(res == null)
                {
                    return NotFound("Map value Does Not Exist");
                }
                _context.RemoveRange(res);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }


    }
}