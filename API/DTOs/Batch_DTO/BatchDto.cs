using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Batch_DTO
{
    public class BatchDto
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public string StartDate {get; set; }
        public string EndDate {get; set; }
        public int Capacity {get; set; }
        public string Technology {get; set; }
        public string Skill_Name {get; set; }
    }
}