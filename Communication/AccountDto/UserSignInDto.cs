using System.ComponentModel.DataAnnotations;

namespace Communication.AccountDto
{
    public class UserSignInDto
    {
        [MaxLength(80)]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(100)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
