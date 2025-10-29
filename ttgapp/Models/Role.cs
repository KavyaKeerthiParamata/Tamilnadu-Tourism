using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ttgapp.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Empty Role name is not allowed")]
        [MaxLength(20, ErrorMessage = "Invalid")]
        public string RoleName { get; set; } = string.Empty;

        public List<User> users { get; set; }

    }
}
