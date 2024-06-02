using Hamro_Pasal.DTO;
using Hamro_Pasal.Models;

namespace Hamro_Pasal.Interfaces
{
    public interface IHomeInterface
    {

        ICollection<AdsFirstLookDTO> GetAdsByLocation(Location location);

        ICollection<AdsFirstLookDTO> GetAdsByLocation(Location location,string user);


                             ICollection<AdsFirstLookDTO> GetAdsRandomly();

        ICollection<AdsFirstLookDTO> GetAdsRandomly(string user);


        ProfileDTO GetProfile(string user);

        ProfileDTO EditProfile(ProfileDTO profile, string user);

        bool DeleteUser(string user);
        ICollection<AdsFirstLookDTO> GetMyAds(string user);

        


    }
}
