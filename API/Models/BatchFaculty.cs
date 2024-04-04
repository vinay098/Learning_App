using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class BatchFaculty
    {
        public string FacultyId {get;set;}
        public User User {get;set;}
        public int BatchId {get;set;}
        public Batch Batch {get;set;}
    }
}