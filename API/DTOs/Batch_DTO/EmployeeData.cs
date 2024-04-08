using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Batch_DTO
{
    public class EmployeeData
    {
        public int BatchId {get;set;}
        public string BatchName {get;set;}
        public string Start {get;set;}
        public string End {get;set;}
        public int Capacity {get;set;}
        public string Technology {get;set;}
        public List<string> ModuleName {get;set;}=new List<string>();
        public List<string> ModuleLevel {get;set;}=new List<string>();
    }
}