using Hamro_Pasal.DTO;

namespace Hamro_Pasal.Interfaces
{
    public interface IUserDetailsInterface
    {
        Task<UserManagerResponse> AddUserDetailsAsync(UserDetailsDTO userDetail);


        Task<UserManagerResponse> UpdateUserDetailsAsync(UserDetailsDTO userDetail);

        bool save();
    }
}
