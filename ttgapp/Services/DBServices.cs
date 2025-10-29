using Microsoft.EntityFrameworkCore;
using ttgapp.Dal;
using ttgapp.Models;

namespace ttgapp.Services
{
    public class DBServices
    {
        private readonly TTGContext _dbcontext;

        public DBServices()
        {
            var configuration=new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string conStr = configuration.GetConnectionString("SQLServerConnection");
            var contextOptions = new DbContextOptionsBuilder<TTGContext>().UseSqlServer(conStr).Options;
            _dbcontext = new TTGContext(contextOptions);
        }

        public List<Slider> GetAllSliders()
        {
            return (from s in _dbcontext.sliders
                        orderby s.DisplayOrderNo
                        select s).ToList();
        }

        public List<TouristPlaceType> GetAllTouristPlaceTypes()
        {
            return _dbcontext.touristPlaceTypes.ToList();
        }

        public List<TouristPlace> GetTouristPlacesByType(int typeId)
        {
            return _dbcontext.touristPlaces
                .Where(tp => tp.TPTypeId == typeId && tp.TPStatus == true)
                .Include(tp => tp.touristPlaceType)
                .ToList();
        }
        public List<TouristPlace> GetPopularTouristPlaces()
        {
            return _dbcontext.touristPlaces
                .Where(tp => tp.IsPopular == true && tp.TPStatus == true)
                .Include(tp => tp.touristPlaceType)
                .Take(6)
                .ToList();
        }


    }
}
