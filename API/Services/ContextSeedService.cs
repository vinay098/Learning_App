using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ContextSeedService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ContextSeedService(AppDbContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager
        )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitalizeContextAsync()
        {
            if(_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() >0)
            {
                await _context.Database.MigrateAsync();
            }
            if(!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole{
                    Name = SD.AdminRole,
                });
                await _roleManager.CreateAsync(new IdentityRole{
                    Name = SD.FacultyRole,
                });
                await _roleManager.CreateAsync(new IdentityRole{
                    Name = SD.EmployeeRole,
                });
            }

            if(!_userManager.Users.AnyAsync().GetAwaiter().GetResult())
            {
                var admin = new User
                {
                    FirstName="Admin",
                    LastName ="User",
                    UserName = "admin1",
                    Email="admin@gmail.com",
                    Dob = new DateTime(1980, 12, 31),
                    Gender = "male",
                };

                await _userManager.CreateAsync(admin,"Admin@123");
                await _userManager.AddToRolesAsync(admin,new[] {SD.AdminRole,SD.FacultyRole,SD.EmployeeRole});
                await _userManager.AddClaimsAsync(admin,new Claim[] {
                    new Claim(ClaimTypes.Email,admin.Email),
                    new Claim(ClaimTypes.Surname,admin.LastName)
                });

                var Faculty = new User
                {
                    FirstName="Faculty",
                    LastName ="User",
                    UserName = "faculty1",
                    Email="faculty@gmail.com",
                    Dob = new DateTime(1985, 12, 31),
                    Gender = "male",
                };

                await _userManager.CreateAsync(Faculty,"Faculty@123");
                await _userManager.AddToRoleAsync(Faculty,SD.FacultyRole);
                await _userManager.AddClaimsAsync(Faculty,new Claim[] {
                    new Claim(ClaimTypes.Email,Faculty.Email),
                    new Claim(ClaimTypes.Surname,Faculty.LastName)
                });

                  var Employee = new User
                {
                    FirstName="Employee",
                    LastName ="User",
                    UserName = "employee1",
                    Email="employee@gmail.com",
                    Dob = new DateTime(1993, 12, 31),
                    Gender = "male",
                };

                await _userManager.CreateAsync(Employee,"Employee@123");
                await _userManager.AddToRoleAsync(Employee,SD.EmployeeRole);
                await _userManager.AddClaimsAsync(Employee,new Claim[] {
                    new Claim(ClaimTypes.Email,Employee.Email),
                    new Claim(ClaimTypes.Surname,Employee.LastName)
                });

            }
        }
    }
}