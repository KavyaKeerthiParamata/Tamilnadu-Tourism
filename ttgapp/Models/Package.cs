using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ttgapp.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [Required(ErrorMessage ="Empty value is not allowed")]
        [MaxLength(50, ErrorMessage ="Invalid Value")]
        public string PackageName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty value is not allowed")]
        [MaxLength(1000, ErrorMessage = "Invalid Value")]
        public string PackageDescription { get; set; } = string.Empty;

       //[Required(ErrorMessage = "Empty value is not allowed")]
        [MaxLength(500, ErrorMessage = "Invalid Value")]
        public string? PackageImagepath { get; set; }

        [Required(ErrorMessage = "Empty value is not allowed")]
        [MaxLength(500, ErrorMessage = "Invalid Value")]
        public string PackageVideopath { get; set; }= string.Empty;


        [Required(ErrorMessage = "Empty value is not allowed")]
        [Range(1,100000,ErrorMessage ="Invalid Value")]
        public int PackagePrice { get; set; }
        public bool PackageStatus { get; set; }

        //for package image

        [NotMapped]
        public IFormFile? PackageImage { get; set; }
    }
}
