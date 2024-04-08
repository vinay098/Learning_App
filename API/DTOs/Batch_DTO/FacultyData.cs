using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Batch_DTO
{
    public class FacultyData
    {
        public int Batch_Id {get;set;}
        public string Batch_Name {get;set;}
        public int Capacity {get;set;}
        public string Start {get;set;}
        public string End {get;set;}
        public string Technology {get;set;}
    }
}