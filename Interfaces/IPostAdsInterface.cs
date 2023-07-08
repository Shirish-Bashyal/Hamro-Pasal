using Hamro_Pasal.DTO;

namespace Hamro_Pasal.Interfaces
{
    public interface IPostAdsInterface
    {

        Task<UserManagerResponse> PostAds(PostAdsDTO postDetails, string user);

        bool save();
    }
}
