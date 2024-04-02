using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.MapperDto
{
    public class MapSkillmoduleDto
    {
        public string ModuleId {get;set;}
        public List<int> SkillId {get;set;} = new List<int>();
    }
}