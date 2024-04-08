using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Batch_DTO
{
    public class AssignBatchDisplay
    {
        public int Batch_Id {get;set;}
        public string Batch_Name {get;set;}
        public string StartDate {get;set;}
        public string EndDate {get;set;}
        public int Capacity {get;set;}
        public string Technology {get;set;}
        public List<string> Faculty {get;set;} = new List<string>();
    }
}