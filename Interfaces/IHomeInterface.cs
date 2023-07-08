using Hamro_Pasal.DTO;
using Hamro_Pasal.Models;

namespace Hamro_Pasal.Interfaces
{
    public interface IHomeInterface
    {

        ICollection<AdsFirstLookDTO> GetAdsByLocation(Location location);

        ICollection<AdsFirstLookDTO> GetAdsRandomly();

    }
}
