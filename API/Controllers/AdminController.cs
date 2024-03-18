using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<User> userManager
        ,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("get-members")]
        public async Task<ActionResult<IList<MemberDto>>> GetAllMembersAsync()
        {
            try
            {
                var members = await _userManager.Users.ToListAsync();
                var viewDto = new List<MemberDto>();
                foreach(var val in members)
                {
                    var dto = new MemberDto
                    {
                        UserId = val.Id,
                        FirstName = val.FirstName,
                        LastName = val.LastName,
                        UserName = val.UserName,
                        Gender = val.Gender,
                        DOB = Convert.ToString(val.Dob),
                        Email = val.Email,
                        Roles = _userManager.GetRolesAsync(val).GetAwaiter().GetResult()
                    };
                    viewDto.Add(dto);
                }
                return Ok(viewDto);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}