namespace ttgapp.Models
{
    public class SearchResultsViewModel
    {
        public string Query { get; set; }

        public List<TouristPlace> TouristPlaces { get; set; } = new List<TouristPlace>();

        public List<Package> Packages { get; set; } = new List<Package>();

        public List<ImageGallery> ImageGalleries { get; set; } = new List<ImageGallery>();
    }
}
