using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.AdminDto;
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
        , RoleManager<IdentityRole> roleManager)
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
                foreach (var val in members)
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
                        Roles = _userManager.GetRolesAsync(val).GetAwaiter().GetResult(),
                        IsApproved=val.LockoutEnabled
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

        [HttpPut("approve-user/{id}")]
        public async Task<ActionResult> ApproveUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user != null)
                {
                    await _userManager.SetLockoutEnabledAsync(user, true);
                }


                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpPut("disapprove-user/{id}")]
        public async Task<ActionResult> DisApproveUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user != null)
                {
                    await _userManager.SetLockoutEnabledAsync(user, false);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

        }

        [HttpGet("get-faculty")]
        public async Task<ActionResult<IList<MemberDto>>> GetAllFaculty()
        {
            try
            {
                var facultyMembers = _userManager.GetUsersInRoleAsync(SD.FacultyRole).Result;
                var viewDto = new List<MemberDto>();
                foreach (var user in facultyMembers)
                {
                    var dto = new MemberDto
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        Gender = user.Gender,
                        DOB = Convert.ToString(user.Dob),
                        Email = user.Email,
                        Roles = await _userManager.GetRolesAsync(user)
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

        [HttpGet("get-employee")]
        public async Task<ActionResult<IList<MemberDto>>> GetAllEmployee()
        {
            try
            {
                var employeeMembers = _userManager.GetUsersInRoleAsync(SD.EmployeeRole).Result;
                var viewDto = new List<MemberDto>();
                foreach (var user in employeeMembers)
                {
                    var dto = new MemberDto
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        Gender = user.Gender,
                        DOB = Convert.ToString(user.Dob),
                        Email = user.Email,
                        Roles = await _userManager.GetRolesAsync(user)
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

        [HttpGet("get-roles")]
        public async Task<ActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpPut("update-role/{user_id}")]
        public async Task<ActionResult> UpdateRole(string user_id, RolenameDto obj)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Contains(obj.RoleName))
            {
                return Ok("User already belongs to the specified role.");
            }
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles.ToArray());
            }
            var addToRoleResult = await _userManager.AddToRoleAsync(user, obj.RoleName);
            if (!addToRoleResult.Succeeded)
            {
                return BadRequest(addToRoleResult.Errors);
            }

            return Ok(addToRoleResult);
        }

        [HttpPost("add-admin")]
        public async Task<ActionResult> AddAdminUser(AddAdminDto obj)
        {
            if (await CheckEmailAsync(obj.Email))
            {
                return BadRequest("Email already Exist");
            }
            if (await CheckContactNumber(obj.PhoneNumber))
            {
                return BadRequest("Phone Number already Exist");
            }
            var registerUser = new User
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Dob = Convert.ToDateTime(obj.DOB),
                Gender = obj.Gender,
                PhoneNumber = obj.PhoneNumber,
                Email = obj.Email,
                UserName = obj.UserName
            };
            var res = await _userManager.CreateAsync(registerUser, obj.Password);
            if (!res.Succeeded) return BadRequest(res.Errors);
            await _userManager.AddToRoleAsync(registerUser, SD.AdminRole);
            return Ok(new JsonResult(new
            {
                title = "Account Registred",
                message = "Your account has been registered"
            }));
        }

        private async Task<bool> CheckEmailAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }
        private async Task<bool> CheckContactNumber(string mobile)
        {
            return await _userManager.Users.AnyAsync(x => x.PhoneNumber == mobile);
        }

    }
}