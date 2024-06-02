using Hamro_Pasal.Data;
using Hamro_Pasal.DTO;
using Hamro_Pasal.Interfaces;
using Hamro_Pasal.Models;
using Microsoft.EntityFrameworkCore;

namespace Hamro_Pasal.Repositories
{
    public class AdsSelectRepository : IAdsSelectInterface
    {
        private readonly ApplicationDbContext _context;
        public AdsSelectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public AdsDetailDTO AdsDetails(int id)
        {
            // var result = _context.tbl_ads.Where(a => a.Id == id).ToList().FirstOrDefault();
            var result = _context.tbl_ads.      //using egar loading to get the values from foreign key
                                                //lazy loading is deafult which doesnot loads the foreign key datas so must include the valuse that are needed as below
            Include(a => a.Ad_by_user)
            .Include(a => a.Ad_Location)
            .Include(a=>a.Category_ad)
            
            .FirstOrDefault(a => a.Id == id);



            if (result == null)  // null aayo vane page ma vako id mistake xa vanne bujna paryo ani tei anusar kei solution chaiyo
                return null;


           
            var viewResult = new AdsDetailDTO
            {
                Id=result.Id,
                PhoneNumberOfSeller=result.Ad_by_user.PhoneNumber,
                AdByUser=result.Ad_by_user.Name,
                AdDescription=result.AdDescription,
                AdsAddress=result.AdAddress,
                CreatedDate=result.CreatedDate,
                AdTitle=result.AdTitle,
                Price=result.Price,
                ImageData=result.Picture,
                

            };

            return viewResult;







        }

        public bool DeleteAd(int id)
        {

            var result = _context.tbl_ads.Find(id);

            _context.tbl_ads.Remove(result);

            var exitStatus = Convert.ToBoolean(_context.SaveChanges()) ? true : false;
            return exitStatus;




        }
    }
}
