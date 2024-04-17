using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Batch_DTO
{
    public class EmployeeResult
    {
        public IEnumerable<EmployeeData>batches { get; set; }
        public int TotalCount {get; set; }
        public int TotalPage {get; set; }
    }
}