using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public string BatchName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public  string Technology { get; set;}
        public string UserId { get; set; }
        public User Users { get; set; }
        public ICollection<BatchModule> BatchModules { get; set; }
        public ICollection<BatchFaculty> BatchFaculties {get;set;}
        
    }
}