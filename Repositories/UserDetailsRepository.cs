using Hamro_Pasal.Data;
using Hamro_Pasal.DTO;
using Hamro_Pasal.Interfaces;
using Hamro_Pasal.Models;
using Microsoft.AspNetCore.Identity;

namespace Hamro_Pasal.Repositories
{
    public class UserDetailsRepository : IUserDetailsInterface

    {
        private ApplicationDbContext _context;
        private UserManager<Authenticate> _userManager;

        public UserDetailsRepository(ApplicationDbContext context, UserManager<Authenticate> userManager)
        {
            _context = context;
            _userManager = userManager;

        }


        //adding details for the first time
        public async Task<UserManagerResponse> AddUserDetailsAsync(UserDetailsDTO userDetail)
        {
            var user = new UserDetails  // creating an instance of userdetails to save to database
            {
                Name = userDetail.Name,
                AboutMe = userDetail.AboutMe,
                PhoneNumber = userDetail.PhoneNumber,
                Address = userDetail.Address,
                Email = userDetail.Email


            };
           
            string id_from_cookies = "1"; // cookies bata web token nikalne ani deseralise garne ani id no value nikalne


            var userInfo = await _userManager.FindByIdAsync(id_from_cookies);  //taking out details of the user from identity_user table
            user.UserData=userInfo;      // adding the indentity details to the user details table

            _context.tbl_user_details.Add(user);    //adding user to database

            _context.SaveChanges();


            throw new NotImplementedException();
        }


        //saving the database
        public bool save()    // should return true or false according to the result
        {
            throw new NotImplementedException();
        }



        //updating the details
        public async Task<UserManagerResponse> UpdateUserDetailsAsync(UserDetailsDTO userDetail)
        {
            var user = new UserDetails  // creating an instance of userdetails to save to database
            {
                Name = userDetail.Name,
                AboutMe = userDetail.AboutMe,
                PhoneNumber = userDetail.PhoneNumber,
                Address = userDetail.Address,
                Email = userDetail.Email


            };


            //identity wala table vo value user le change gardaina so yo cookies bata id nikalera garna parena????
            //string id_from_cookies = "1"; // cookies bata web token nikalne ani deseralise garne ani id no value nikalne


            //var userInfo = await _userManager.FindByIdAsync(id_from_cookies);  //taking out details of the user from identity_user table
            //user.UserData = userInfo;      // adding the indentity details to the user details table

            _context.Update(user);    //updating user to database

            _context.SaveChanges();



            throw new NotImplementedException();
        }
    }
}
