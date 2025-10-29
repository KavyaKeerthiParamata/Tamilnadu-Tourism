using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ttgapp.Models
{
    public class Slider
    {
        [Key]
        public int SliderId {  get; set; }

        [Required]
        public string? Name { get; set; }

        public string? DisplayText { get; set; }

        public string? LinkText { get; set; }

        public string? SliderImagePath { get; set; }

        [Required]

        public bool Status { get; set; }

        [Required]

        public int DisplayOrderNo { get; set; }

        [NotMapped]
        public IFormFile? SliderImage { get; set; }
    }
}
