using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class LearnModule
    {
        public int Id { get; set; }
        public string Module_Name { get; set; }
        public string Proefficiency_level { get; set; }
        public string Learning_Type { get; set; }
        public string Certification_Type { get; set; }
        public string UserId {get; set; }
        public User Users { get; set; }
        public ICollection<SkillModule> SkillModules { get; set; }
        public ICollection<BatchModule> BatchModules { get; set; }
    }
}