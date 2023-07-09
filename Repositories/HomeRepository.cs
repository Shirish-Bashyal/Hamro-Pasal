using Hamro_Pasal.Data;
using Hamro_Pasal.DTO;
using Hamro_Pasal.Interfaces;
using Hamro_Pasal.Models;
using System.Runtime.InteropServices;

namespace Hamro_Pasal.Repositories
{
    public class HomeRepository : IHomeInterface
    {

        private readonly ApplicationDbContext _context;
        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;

        }






        public ICollection<AdsFirstLookDTO> GetAdsByLocation(Location location)
        {


            var ads = _context.tbl_ads.Where(x => x.Ad_Location.City == location.City).OrderBy(r => Guid.NewGuid()).Take(10).Select(x => new AdsFirstLookDTO()
            {
                Id = x.Id,
                AdTitle = x.AdTitle,
                Price = x.Price,
            }).ToList();   // k gareko vanda, database bata location same vako lai fetch gareko, ani random order ma rakheko guid use garera ani top 10 ligeko  ani mapping gareko;


            //var ads = _context.tbl_ads.Where(x => x.Ad_Location.City == location.City).Select (x => new AdsFirstLookDTO()
            //{
            //    AdTitle = x.AdTitle,
            //    Price = x.Price,
            //}).ToList();   // k gareko vanda, database bata location same vako lai fetch gareko ani mapping gareko


            if (ads == null)
            { // yedi null xa vane random location bata ads fetch garera return garne 




                var ads1 = _context.tbl_ads.OrderBy(r => Guid.NewGuid()).Take(10).Select(x => new AdsFirstLookDTO()
                {
                    Id= x.Id,
                    AdTitle = x.AdTitle,
                    Price = x.Price,
                }).ToList();
                return ads1;
            }



            return ads;









        }

        public ICollection<AdsFirstLookDTO> GetAdsRandomly()
        {

            var ads = _context.tbl_ads.OrderBy(r => Guid.NewGuid()).Take(10).Select(x => new AdsFirstLookDTO()
            {
                AdTitle = x.AdTitle,
                Price = x.Price,
            }).ToList();

            return ads;




        }
    }
}

