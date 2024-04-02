using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Skills_DTO;
using API.Models;

namespace API.Interface
{
    public interface ISkills
    {
        Task DeleteSkillsAsync(Skills skill);
        Task<Skills> GetSkillsById(int id);
        Task<Skills> AddSkillsAsync(string id,SkillDto skill);
        Task UpdateSkillsAsync(int id,SkillDto skill);
        Task<List<SkillDto>> GetAllSkillsAsync();
        Task<SkillDto> GetSkillDtoById(int id);
    }
}