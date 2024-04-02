using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UpdateUserDto
    {
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string PhoneNumber {get;set;}
        [StringLength(20,MinimumLength =5,ErrorMessage ="Minimum length must be {2} and maximum length {1} characters.")]
        public string UserName {get;set;}
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$",ErrorMessage ="Invalid Email Address")]
        public string Email {get;set;}
    }
}