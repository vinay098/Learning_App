using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.DTOs.MapperDto
{
    public class MapBatchModule
    {
        public string Batch_id { get; set; }
        public List<int> Module_Id { get; set; }=new List<int>();
    }
}