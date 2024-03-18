using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.MapperDto
{
    public class BatchModuleDisplayDto
    {
        public string BatchName {get; set; }
        public int Capacity {get; set; }
        public string Technology {get; set; }
        public string Batch_Start {get; set; }
        public string Batch_End {get; set; }
        public string ModuleName {get; set; }
        public string Level {get; set; }
        public string Certification_Type {get; set;}
    }
}