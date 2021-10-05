using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Communication
{
    public class UserSignUpDto
    {
        [MaxLength(80)]
        public string Username { get; set; }
        [MaxLength(80)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(100)]
        [MinLength(8)]
        public string Password { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}
