using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ttgapp.Models
{
    public class ImageGallery
    {
        [Key]
        public int ImageGalleryId { get; set; }

        [Required(ErrorMessage = "Empty value is not allowed")]
        [MaxLength(50, ErrorMessage = "Invalid Value")]
        public string ImageGalleryName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty value is not allowed")]
        [MaxLength(50, ErrorMessage = "Invalid Value")]
        public string ImageGalleryFolderPath { get; set; } = string.Empty;
        public bool ImageGalleryStatus { get; set; }

        public int TPId { get; set; }

        [ForeignKey("TPId")]
        public TouristPlace? TouristPlace { get; set; }
    }
}
