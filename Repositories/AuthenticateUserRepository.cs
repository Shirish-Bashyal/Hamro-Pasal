using AutoMapper;
using Hamro_Pasal.Data;
using Hamro_Pasal.DTO;
using Hamro_Pasal.Interfaces;
using Hamro_Pasal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hamro_Pasal.Repositories
{
    public class AuthenticateUserRepository : IUserInterface
    {
        private ApplicationDbContext _context;
        private UserManager<Authenticate> _user;
       // private Mapper _mapper;
        private IConfiguration _configuration;
        public AuthenticateUserRepository(ApplicationDbContext context, UserManager<Authenticate> user, IConfiguration configuration)
            
        {
            _context = context;
            _user = user;
           // _mapper = mapper;
            _configuration = configuration;
        }

        public bool CheckUserDetails(string Email)
        {

            var result=_context.tbl_user_details.Where(x => x.Email == Email).ToList();
            if(result==null)
                return false;
            return true;
        }




        //register user
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterDTO model)
        {

            var doesExist = await _user.FindByEmailAsync(model.Email);
            if (doesExist != null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Email already used"


                };

            var user = new Authenticate
            {
                Email = model.Email,
                UserName=model.Name,



            };

            var result = await _user.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var userDetails = new UserDetails();
                var userData=await _user.FindByEmailAsync(model.Email);
                userDetails.PhoneNumber=model.PhoneNumber;
                userDetails.Address = model.Address;
                userDetails.Email = model.Email;
                userDetails.AboutMe = model.AboutMe;
                userDetails.Name= model.Name;
                userDetails.UserData=userData;
                _context.tbl_user_details.Add(userDetails);
                _context.SaveChanges();



                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "User is created"


                };
            }

            else
                return new UserManagerResponse
                {
                    IsSuccess = false,
                };









        }



        //signin user
        public  async Task<UserManagerResponse> SignInUserAsync(SigninDTO model)
        {
            var user = await _user.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };
            }

            var checkPassword = await _user.CheckPasswordAsync(user, model.Password);
            if (checkPassword!=true)
                return new UserManagerResponse
                {
                    Message = "Invalid password",

                    IsSuccess = false,



                };



            var claims = new[]
           {
                new Claim("Email", model.Email),
                new Claim("UserId", user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);


          
            


            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo

            };













             // token, cookies, redirection
        }


        //signout user
        public UserManagerResponse SignoutUser()
        {
            throw new NotImplementedException();// signin vayesi balla yo
        }



        
























    }
}
