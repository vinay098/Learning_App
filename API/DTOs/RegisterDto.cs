using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set;}
        [Required]
        public string LastName { get; set;}
        [Required]
        [StringLength(20,MinimumLength =5,ErrorMessage ="Minimum length must be {2} and maximum length {1} characters.")]
        public string UserName {get; set;}
        public string DOB {get ; set;} 
        public string Gender {get; set;}
        [Required]
        public string ContactNumber {get; set;}
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$",ErrorMessage ="Invalid Email Address")]
        public string Email {get; set;}
        [Required]
        [StringLength(15,MinimumLength =6,ErrorMessage ="Password length must be atleast {2} and Maximum {1} characters")]
        public string Password {get; set;}

        [Required]
        public string Role {get; set;}
    }
}