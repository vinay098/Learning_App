using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.DTOs.Batch_DTO
{
    public class BatchResult
    {
        public IEnumerable<BatchDto>batches { get; set; }
        public int TotalCount {get; set; }
        public int TotalPage {get; set; }
    }
}