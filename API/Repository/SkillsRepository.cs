using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.DTOs.Skills_DTO;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class SkillsRepository : ISkills
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SkillsRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Skills> AddSkillsAsync(string id,SkillDto skill)
        {
             var addedskill= new Skills{
                Skill_Name= skill.Name,
                Skill_Family = skill.Family,
                UserId = id
            };
            _context.Skills.Add(addedskill);
            await _context.SaveChangesAsync();

            return addedskill;
        }

        public async Task DeleteSkillsAsync(Skills skill)
        {
             _context.Skills.Remove(skill);
             await _context.SaveChangesAsync();
        }

        public async Task<List<SkillDto>> GetAllSkillsAsync()
        {
            var skills = await _context.Skills.ToListAsync();
            var res =  _mapper.Map<List<Skills>,List<SkillDto>>(skills);

            return res;
        }

        public async Task<SkillDto> GetSkillDtoById(int id)
        {
            var skills = await _context.Skills.ToListAsync();
            var ans =  _mapper.Map<List<Skills>,List<SkillDto>>(skills);
            var res = ans.FirstOrDefault(x=>x.Id==id);

            return res;
        }

        public async Task<Skills> GetSkillsById(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            return skill;
        }


        public async Task UpdateSkillsAsync(int id, SkillDto skill)
        {
            var updateSkill = await _context.Skills.FindAsync(id);
            if(updateSkill == null){
                 throw new Exception("Skills Not Found");
            }
            updateSkill.Skill_Name =skill.Name;
            updateSkill.Skill_Family =skill.Family;

            await _context.SaveChangesAsync();
        }
    }
}