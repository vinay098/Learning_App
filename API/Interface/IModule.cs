using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Module_DTO;
using API.Models;

namespace API.Interface
{
    public interface IModule
    {
        Task<ModuleDto> AddModuleAsync(string user_id,ModuleDto obj);
        Task<IList<ModuleDto>> GetAllModulesAsync();
        Task<ModuleDto>GetModuleDtoByIdAsync(int id);
        Task<LearnModule> GetModuleByIdAsync(int id);
        Task DeleteModuleAsync(LearnModule obj);
        Task UpdateModuleAsync(int id,string user_id,ModuleDto obj);
         Task<IList<string>> GetModuleName(int module_id);
    }
}