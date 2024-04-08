using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SkillModule
    {
        public int SkillId {get;set;}
        public Skills Skills {get;set;}
        public int ModuleId {get;set;}
        public LearnModule Module {get;set;}
        public string UserId {get;set;}
        public User Users {get;set;}
    }
}