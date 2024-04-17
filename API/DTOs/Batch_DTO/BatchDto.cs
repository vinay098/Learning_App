
using Sieve.Attributes;

namespace API.DTOs.Batch_DTO
{
    public class BatchDto
    {
        public int Id {get; set; }
        
        [Sieve(CanFilter =true,CanSort =true)]
        public string Name {get; set; }
        public string StartDate {get; set; }
        public string EndDate {get; set; }
        public int Capacity {get; set; }
        public string Technology {get; set; }
    }
}