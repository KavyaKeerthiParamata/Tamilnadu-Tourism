using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ttgapp.Models
{
    public class VirtualTour
    {
        [Key]
        public int VirtualTourId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? ThumbnailImagePath { get; set; } // Optional preview image

        public string? VideoEmbedCode { get; set; } // Full iframe HTML or YouTube embed URL

        public string? ExternalTourLink { get; set; } // For 360° web-based tours

        [ForeignKey("TouristPlace")]
        public int TPId { get; set; }

        public TouristPlace TouristPlace { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
