
using System.Security.Claims;
using API.Context;
using API.DTOs.Batch_DTO;
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
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IModule _modulerepo;
        public readonly IMapper _mapper;

        public MapperController(AppDbContext context,
        IHttpContextAccessor accessor,
        IModule modulerepo, IMapper mapper)
        {
            _mapper = mapper;
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
                    ob.SkillId = val;
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
                var batches = await _context.Batches
                .Include(b => b.BatchModules)
                .ThenInclude(bm => bm.Module).ToListAsync();
                var ans = _mapper.Map<List<Batch>, List<BatchModuleDisplayDto>>(batches);
                return Ok(ans);
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
                var modules = await _context.Modules.Include(m=>m.SkillModules)
                .ThenInclude(sm=>sm.Skills).ToListAsync();
                  var ans = _mapper.Map<List<LearnModule>, List<SkillModuleValuesDto>>(modules);
                return Ok(ans);
                
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
                    ob.ModuleId = Convert.ToInt32(obj.ModuleId);
                    ob.SkillId = val;
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
                var res = await _context.SkillModules.Where(m => m.ModuleId == id).ToListAsync();
                if (res == null)
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

        [HttpPost("assign-batch-to-faculty")]
        public async Task<ActionResult> AssignBatch(AssignBatch obj)
        {
            // string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var ob = new BatchFaculty();
                ob.BatchId = obj.BatchID;
                ob.UserId = obj.Faculty_id;
                _context.Add(ob);
                await _context.SaveChangesAsync();
                var res = await _context.BatchFaculties.ToListAsync();
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("buy-batch")]
        public async Task<ActionResult> BuyBatch(BuyBatch obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                // var checkIfUserAlreadyEnrolled = await _context.BatchFaculties
                // .Where(x=>x.UserId == user_Id)
                // .Select(x=>x.BatchId).ToListAsync();

                // foreach(var check in checkIfUserAlreadyEnrolled)
                // {
                //     if(check == obj.BatchId)
                //     {
                //         return BadRequest("You are already Enrolled in this batch");
                //     }
                // }

                var ob = new BatchFaculty();
                ob.BatchId = obj.BatchId;
                ob.UserId = user_Id;

                _context.Add(ob);
                await _context.SaveChangesAsync();

                var batch = await _context.Batches.FirstAsync(x => x.Id == obj.BatchId);
                if (batch.Capacity > 0)
                {
                    batch.Capacity -= 1;
                }
                else
                {
                    return BadRequest("Batch is Full");
                }
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("get-batch-module/{id}")]
        public async Task<ActionResult> GetBatchModuleById(int id)
        {
            try
            {
                var ModuleIds = await _context.BatchModules.Where(b => b.BatchId == id)
                            .Select(b => b.ModuleId).ToListAsync();

                return Ok(ModuleIds);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("update-Batch-Module/{id}")]
        public async Task<ActionResult> UpdateBatchModule(int id, MapBatchModule obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                // 1. Find the Batch
                var batch = await _context.Batches.FindAsync(id);

                if (batch == null)
                {
                    return NotFound("Batch not found.");
                }

                // 2. Retrieve Existing Associations (assuming junction table is BatchModule)
                var existingAssociations = await _context.BatchModules
                    .Where(bm => bm.BatchId == id)
                    .ToListAsync();

                // 3. Compare and Update
                var modulesToAdd = obj.Module_Id.Except(existingAssociations.Select(a => a.ModuleId));
                var modulesToRemove = existingAssociations.Where(a => !obj.Module_Id.Contains(a.ModuleId));

                // Add new module associations
                foreach (var moduleId in modulesToAdd)
                {
                    _context.BatchModules.Add(new BatchModule { BatchId = id, ModuleId = moduleId, UserId = user_Id });
                }

                // Remove unwanted associations
                _context.BatchModules.RemoveRange(modulesToRemove);

                // Save changes to the database
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("get-skill-module/{id}")]
        public async Task<ActionResult> GetSkillModuleById(int id)
        {
            try
            {
                var skillIds = await _context.SkillModules.Where(b => b.ModuleId == id)
                            .Select(b => b.SkillId).ToListAsync();

                return Ok(skillIds);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }


        [HttpPut("update-skill-Module/{id}")]
        public async Task<ActionResult> UpdateSkillModule(int id, MapSkillModule obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                // 1. Find the Module
                var module = await _context.Modules.FindAsync(id);

                if (module == null)
                {
                    return NotFound("Module not found.");
                }

                // 2. Retrieve Existing Associations (assuming junction table is SKillModule)
                var existingAssociations = await _context.SkillModules
                    .Where(bm => bm.ModuleId == id)
                    .ToListAsync();

                // 3. Compare and Update
                var SkillToAdd = obj.SkillId.Except(existingAssociations.Select(a => a.SkillId));
                var SkillToRemove = existingAssociations.Where(a => !obj.SkillId.Contains(a.SkillId));

                // Add new module associations
                foreach (var SkillId in SkillToAdd)
                {
                    _context.SkillModules.Add(new SkillModule { ModuleId = id, SkillId = SkillId, UserId = user_Id });
                }

                // Remove unwanted associations
                _context.SkillModules.RemoveRange(SkillToRemove);

                // Save changes to the database
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