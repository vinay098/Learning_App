
using System.Security.Claims;
using API.Context;
using API.DTOs.MapperDto;
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
        private readonly IMapper _mapper;

        public MapperController(ApplicationDbContext context, 
        IHttpContextAccessor accessor,IMapper mapper)
        {
            _context = context;
            _accessor = accessor;
            _mapper = mapper;
        }

        [HttpPost("skill-module-map")]
        public async Task<ActionResult> MapSkillAndModule(SkillModuleDto obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var skill_id = await _context.Skills
                .Where(s => s.Skill_Name == obj.SkillName)
                .Select(s => s.Id).FirstAsync();

                var module_id = await _context.Modules
                .Where(m => m.Module_Name == obj.ModuleName)
                .Select(m => m.Id).FirstAsync();

                var addedval = new SkillModule
                {
                    SkillId = skill_id,
                    ModuleId = module_id,
                    UserId = user_Id
                };
                _context.Add(addedval);
                await _context.SaveChangesAsync();
                return Ok(addedval);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpGet("get-skill-module")]
        public async Task<ActionResult<IList<SkillModuleValuesDto>>> GetSkillWithModule()
        {
            try
            {
                var res = await _context.SkillModules
                .Include(sm=>sm.Skills)
                .Include(sm=>sm.Module)
                .Where(sm=>(sm.Skills.Id==sm.SkillId) &&(sm.Module.Id==sm.ModuleId)).ToListAsync();

                return Ok(_mapper.Map<List<SkillModule>,List<SkillModuleValuesDto>>(res));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("batch-module-map")]
        public async Task<ActionResult> MapBatchAndModule(BatchModuleInsertDto obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string role = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            try
            {
                var batch_id = await _context.Batches
                .Where(b => b.BatchName == obj.BatchName)
                .Select( b=> b.Id).FirstAsync();

                var module_id = await _context.Modules
                .Where(m => m.Module_Name == obj.ModuleName)
                .Select(m => m.Id).FirstAsync();

                var addedval = new BatchModule
                {
                    BatchId = batch_id,
                    ModuleId = module_id,
                    UserId = user_Id
                };
                _context.Add(addedval);
                await _context.SaveChangesAsync();
                return Ok(addedval);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpGet("get-batch-module")]
        public async Task<ActionResult<IList<BatchModuleDisplayDto>>> GetBatchWithModule()
        {
            try
            {
                var res = await _context.BatchModules
                .Include(sm=>sm.Batch)
                .Include(sm=>sm.Module)
                .Where(sm=>(sm.Batch.Id==sm.BatchId) &&(sm.Module.Id==sm.ModuleId)).ToListAsync();

                return Ok(_mapper.Map<List<BatchModule>,List<BatchModuleDisplayDto>>(res));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

    }
}