using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Context;
using API.DTOs.Module_DTO;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ModuleRepository : IModule
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ModuleRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ModuleDto> AddModuleAsync(string user_id, ModuleDto obj)
        {
            var addedModule = new LearnModule
            {
                Module_Name = obj.Name,
                Proefficiency_level= obj.Level,
                Learning_Type = obj.Learning_Type,
                Certification_Type = obj.Certification_Type,
                UserId = user_id,
            };

            _context.Modules.Add(addedModule);
            await _context.SaveChangesAsync();

            var dto = new ModuleDto
            {
                Id = addedModule.Id,
                Name = addedModule.Module_Name,
                Level = addedModule.Proefficiency_level,
                Learning_Type = addedModule.Learning_Type,
                Certification_Type = addedModule.Certification_Type
            };

            return dto;

        }

        public async Task DeleteModuleAsync(LearnModule obj)
        {
            _context.Modules.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<ModuleDto>> GetAllModulesAsync()
        {
            var module = await _context.Modules.ToListAsync();
            var res = _mapper.Map<List<LearnModule>,List<ModuleDto>>(module);
            return res;
        }

        public async Task<LearnModule> GetModuleByIdAsync(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            return module;
        }

        public async Task<ModuleDto> GetModuleDtoByIdAsync(int id)
        {
            var module = await _context.Modules.ToListAsync();
            var ans = _mapper.Map<List<LearnModule>,List<ModuleDto>>(module);
            var res = ans.FirstOrDefault(x=>x.Id==id);
            return res;
        }

        public async Task UpdateModuleAsync(int id,string user_Id, ModuleDto obj)
        {
           var updateModule = await GetModuleByIdAsync(id);
           if(updateModule == null)
           {
            throw new Exception("Could not found Module");
           }
           
           updateModule.Module_Name = obj.Name;
           updateModule.Learning_Type = obj.Learning_Type;
           updateModule.Proefficiency_level = obj.Level;
           updateModule.Certification_Type =obj.Certification_Type;
           updateModule.UserId = user_Id;
           await _context.SaveChangesAsync();
        }
         public async Task<IList<string>> GetModuleName(int batch_id)
        {
            var ans = await _context.BatchModules
                  .Where(c => c.BatchId == batch_id)
                  .Select(s => s.Module.Module_Name).ToListAsync();

            return ans;
        }
    }
}