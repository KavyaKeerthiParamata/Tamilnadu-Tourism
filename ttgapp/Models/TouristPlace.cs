using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ttgapp.Models
{
    public enum District
    {
        Ariyalur,
        Chengalpattu,
        Chennai,
        Coimbatore,
        Cuddalore,
        Dharmapuri,
        Dindigul,
        Erode,
        Kallakurichi,
        Kancheepuram,
        Karur,
        Krishnagiri,
        Madurai,

        Mayiladuthurai,

        Nagapattinam,

        Kanniyakumari,

        Namakkal,

        Perambalur,

        Pudukkottai,

        Ramanathapuram,
        Ranipet,

        Salem,

        Sivagangai,

        Tenkasi,

        Thanjavur,

        Theni,

        Thiruvallur,

        Thiruvarur,

        Thoothukudi,

        Tiruchirappalli,

        Tirunelveli,
        Tirupathur,

        Tiruppur,

        Tiruvannamalai,

        TheNilgiris,

        Vellore,

        Viluppuram,

        Virudhunagar,
    }
    public class TouristPlace
    {
        [Key]
        public int TPId { get; set; }

        [Required(ErrorMessage ="Empty Not Allowed")]
        [MaxLength(50,ErrorMessage ="Invalid Size")]
        public string TPName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty Not Allowed")]
        [MaxLength(1500, ErrorMessage = "Invalid Size")]
        public string TPDescription { get; set; } = string.Empty;
        public string TPImagePath { get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty Not Allowed")]
        [MaxLength(10, ErrorMessage = "Invalid Size")]
        public string TPlongitude { get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty latitude Not Allowed")]
        [MaxLength(50, ErrorMessage = "Invalid Size")]
        public string TPlatitude { get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty Not Allowed")]
        [MaxLength(500, ErrorMessage = "Invalid Size")]
        public string TPGoogleLink {  get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty Not Allowed")]
        [Range(0,int.MaxValue)]
        public int TPRating { get; set; }
        public bool TPStatus { get; set; }

        [Required(ErrorMessage = "Empty Not Allowed")]
        [MaxLength(150, ErrorMessage = "Invalid Size")]
        public string Location { get; set; }= string.Empty;

        [Required(ErrorMessage = "Empty Not Allowed")]
        public District District {  get; set; }

        [Required(ErrorMessage = "Empty Not Allowed")]
        [MaxLength(20, ErrorMessage = "Invalid Size")]
        public string State {  get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty Not Allowed")]
        [MaxLength(10, ErrorMessage = "Invalid Size")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty Not Allowed")]
        [MaxLength(500, ErrorMessage = "Invalid Size")]
        public string BestTimeToVisit {  get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty TransportDetails Not Allowed")]
        [MaxLength(500, ErrorMessage = "Invalid Size")]
        public string TransportationDetails { get; set; } = string.Empty;

        [Required(ErrorMessage = "Empty Accomodation Details Not Allowed")]
        [MaxLength(500, ErrorMessage = "Invalid Size")]
        public string AccomodationDetails {  get; set; } = string.Empty;

        public bool IsPopular { get; set; } = false;

        public int TPTypeId {  get; set; }

        [ForeignKey("TPTypeId")]
        public TouristPlaceType? touristPlaceType { get; set; }

        [NotMapped]
        public IFormFile? TPImage { get; set; }
    }
}
