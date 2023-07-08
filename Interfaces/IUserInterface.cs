using Hamro_Pasal.DTO;


namespace Hamro_Pasal.Interfaces
{
    public interface IUserInterface
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterDTO model); //task is used for asyncronous operations 

        Task<UserManagerResponse> SignInUserAsync(SigninDTO model);

        //   Task<UserManagerResponse> SignoutAsync();

        bool CheckUserDetails(string Email);

        UserManagerResponse SignoutUser();


    }
}
