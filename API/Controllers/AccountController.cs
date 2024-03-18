using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly JWTService _jwtservice;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public AccountController(JWTService jwtservice,
        SignInManager<User> signInManager,
        UserManager<User> userManager)
        {
            _jwtservice=jwtservice;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [Authorize]
        [HttpGet("refresh-token")]
        public async Task<ActionResult<UserDto>> RefreshToken()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email)?.Value);
            return await CreateAppUserDto(user);
        }

        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto obj)
        {
           var user = await _userManager.FindByNameAsync(obj.UserName);
           if (user == null) return Unauthorized("Invalid UserName Or Password");
           var password = await _signInManager.CheckPasswordSignInAsync(user,obj.Password,false);
           if(!password.Succeeded) return Unauthorized("Invalid UserName Or Password");
           return await CreateAppUserDto(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto obj)
        {
            if(await CheckEmailAsync(obj.Email)  )
            {
                return BadRequest("Email already Exist");
            }
            if(await CheckContactNumber(obj.ContactNumber))
            {
                return BadRequest("Phone Number already Exist");
            }
            var registerUser = new User
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Dob = Convert.ToDateTime(obj.DOB),
                Gender = obj.Gender,
                PhoneNumber = obj.ContactNumber,
                Email = obj.Email,
                UserName=obj.UserName
            };
            var res = await _userManager.CreateAsync(registerUser,obj.Password);
            if(!res.Succeeded) return BadRequest(res.Errors);
            await _userManager.AddToRoleAsync(registerUser,obj.Role);

            return Ok(new JsonResult(new
            {title="Account Registred",
            message="Your account has been registered"
            }));
        }

        private async Task<UserDto> CreateAppUserDto(User obj)
       {
            return new UserDto
            {
                UserName = obj.UserName,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                JWT = await _jwtservice.CreateJWT(obj),
                PhoneNumber = obj.PhoneNumber,
                Email =obj.Email
            };
       }

       private async Task<bool> CheckEmailAsync(string email)
       {
            return await _userManager.Users.AnyAsync(x=>x.Email == email.ToLower());
       }
       private async Task<bool> CheckContactNumber(string mobile)
       {
            return await _userManager.Users.AnyAsync(x=>x.PhoneNumber == mobile);
       }
    }
}