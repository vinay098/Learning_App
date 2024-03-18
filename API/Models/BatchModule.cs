using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Models
{
    public class BatchModule
    {
        public int BatchId { get; set; }
        public Batch Batch { get; set; }
        public int ModuleId {get; set; }
        public LearnModule Module { get; set; }
        public string UserId { get; set; }
        public ICollection<User> Users { get; set;}
    }
}