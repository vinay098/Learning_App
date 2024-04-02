using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.MapperDto
{
    public class SkillModuleValuesDto
    {
        public int ModuleId {get;set;}
        public string ModuleName {get; set;}
        public string Level {get;set;}
        public string Learning_Type {get; set;}
        public string Certification_Type {get; set;}
        public List<string> SkillName {get; set;} = new List<string>();
    }
}