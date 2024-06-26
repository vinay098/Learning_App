using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MemberDto
    {
        public string UserId { get; set; }
        public string FirstName {get; set; }
        public string LastName {get; set; }
        public string UserName {get; set; }
        public string Gender {get; set; }
        public string DOB {get; set; }
        public string Email {get; set; }
        public IList<string> Roles {get; set; }
        public bool IsApproved {get;set;}
    }
}