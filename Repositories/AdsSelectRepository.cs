using Hamro_Pasal.Data;
using Hamro_Pasal.Interfaces;
using Hamro_Pasal.Models;

namespace Hamro_Pasal.Repositories
{
    public class AdsSelectRepository : IAdsSelectInterface
    {
        private readonly ApplicationDbContext _context;
        public AdsSelectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public UserAds AdsDetails(int id)
        {
            var result = _context.tbl_ads.Where(a => a.Id == id).FirstOrDefault();

            if (result == null)  // null aayo vane page ma vako id mistake xa vanne bujna paryo ani tei anusar kei solution chaiyo
                return null;


            return result;






            throw new NotImplementedException();
        }
    }
}
