using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ttgapp.Models
{
    public class TouristPlaceType
    {
        [Key]
        public int TPTypeId { get; set; }

        [Required(ErrorMessage ="Empty TouristPlace Type Is Not Allowed")]
        [MaxLength(20,ErrorMessage ="Invalid Size")]
        public string TPTypeName { get; set; } = string.Empty;
        public string TpTypeImagePath { get; set; } = string.Empty ;

        [Required(ErrorMessage = "Empty TouristPlace Description Is Not Allowed")]
        [MaxLength(500, ErrorMessage = "Inavlid Size")]
        public string TpDescription { get; set; } = string.Empty;
        public bool TPTypeStatus { get; set; }

        [NotMapped]
        public IFormFile? TpTypeImage { get; set; }

        public List<TouristPlace>? touristPlaces { get; set; }
    }
}
