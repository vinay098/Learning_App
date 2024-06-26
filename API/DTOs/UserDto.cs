using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName {get; set;}
        public string JWT{get; set; }
        public List<string> Role {get; set; }
        public string PhoneNumber {get; set; }
        public string Email {get; set; }
        public bool IsApproved {get;set;}
    }
}