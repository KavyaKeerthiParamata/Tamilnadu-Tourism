using System.ComponentModel.DataAnnotations;

namespace ttgapp.Models
{
    public class UserLogin
    {
        [Key]
        [Required(ErrorMessage = "Empty UserId Not Allowed")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty Password Not Allowed")]
        public string Password { get; set; } = string.Empty;
    }
}
