using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.AdminDto
{
    public class AddAdminDto
    {
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string DOB {get;set;}
        public string Gender{get;set;}
        public string Email {get;set;}
        public string PhoneNumber {get;set;}
        public string UserName {get;set;}
        public string Password {get;set;}
    }
}