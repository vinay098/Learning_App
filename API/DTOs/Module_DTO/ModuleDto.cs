using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Module_DTO
{
    public class ModuleDto
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Level {get;set;}
        public string Learning_Type {get;set;}
        public string Certification_Type {get;set;}
    }
}