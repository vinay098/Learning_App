using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs.Module_DTO;
using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly IModule _moduleRepo;
        private readonly IHttpContextAccessor _accessor;
        
        public ModuleController(IModule moduleRepo,IHttpContextAccessor accessor)
        {
            _moduleRepo = moduleRepo;
            _accessor = accessor;
        }

        [HttpPost("add-module")]
        public async Task<ActionResult> AddModule(ModuleDto module)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var res = await _moduleRepo.AddModuleAsync(user_Id,module);
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAllModules()
        {
            try
            {
                var res = await _moduleRepo.GetAllModulesAsync();
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("get-module/{id}")]
        public async Task<ActionResult> GetModuleDtoById(int id)
        {
            try
            {
                var res = await _moduleRepo.GetModuleDtoByIdAsync(id);
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("update-module/{id}")]
        public async Task<ActionResult> UpdateModule(int id,ModuleDto obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _moduleRepo.UpdateModuleAsync(id,user_Id,obj);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("delete-module/{id}")]
        public async Task<ActionResult> DeleteModule(int id)
        {
            try
            {
                var module = await _moduleRepo.GetModuleByIdAsync(id);
                if(module == null)
                {
                    return NotFound(new
                    {
                        status=404,
                        message = "batch not found"
                    });
                }
                await _moduleRepo.DeleteModuleAsync(module);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}