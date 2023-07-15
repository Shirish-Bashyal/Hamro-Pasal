using Hamro_Pasal.DTO;
using Hamro_Pasal.Models;

namespace Hamro_Pasal.Interfaces
{
    public interface IAdsSelectInterface
    {


        AdsDetailDTO AdsDetails(int id);

        bool DeleteAd(int id);

    }
}
