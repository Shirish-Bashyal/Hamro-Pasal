using Hamro_Pasal.Data;
using Hamro_Pasal.DTO;
using Hamro_Pasal.Interfaces;
using Hamro_Pasal.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;

namespace Hamro_Pasal.Repositories
{
    public class HomeRepository : IHomeInterface
    {

        private readonly ApplicationDbContext _context;
        private UserManager<Authenticate> _userManager;

        public HomeRepository(ApplicationDbContext context, UserManager<Authenticate> userManager)
        {
            _context = context;
            _userManager = userManager;

        }






        public ICollection<AdsFirstLookDTO> GetAdsByLocation(Location location)
        {


            var ads = _context.tbl_ads.Where(x => x.Ad_Location.City == location.City).OrderBy(r => Guid.NewGuid()).Take(10).Select(x => new AdsFirstLookDTO()
            {
                Id = x.Id,
                AdTitle = x.AdTitle,
                Price = x.Price,
                ImageData=x.Picture
            }).ToList();   // k gareko vanda, database bata location same vako lai fetch gareko, ani random order ma rakheko guid use garera ani top 10 ligeko  ani mapping gareko;

            var count = ads.Count();

            //var ads = _context.tbl_ads.Where(x => x.Ad_Location.City == location.City).Select (x => new AdsFirstLookDTO()
            //{
            //    AdTitle = x.AdTitle,
            //    Price = x.Price,
            //}).ToList();   // k gareko vanda, database bata location same vako lai fetch gareko ani mapping gareko


            if (ads==null ||count==1)
            { // yedi null xa vane random location bata ads fetch garera return garne 




                var ads1 = _context.tbl_ads.OrderBy(r => Guid.NewGuid()).Take(10).Select(x => new AdsFirstLookDTO()
                {
                    Id= x.Id,
                    AdTitle = x.AdTitle,
                    Price = x.Price,
                    ImageData = x.Picture

                }).ToList();



                var count1 = ads1.Count();
                if (count1 % 2 == 0)
                    return ads1;

                else
                {
                    Random random = new Random();
                    int randomIndex = random.Next(0, count1); // Generate a random index within the list bounds

                    ads1.RemoveAt(randomIndex);
                    return ads1;
                }

               
            }


            
            if (count % 2==0)
                return ads;

            else
            {
                Random random = new Random();
                int randomIndex = random.Next(0, count); // Generate a random index within the list bounds

                ads.RemoveAt(randomIndex);
                return ads;
            }












        }

        public ICollection<AdsFirstLookDTO> GetAdsRandomly()
        {

            var ads = _context.tbl_ads.OrderBy(r => Guid.NewGuid()).Take(10).Select(x => new AdsFirstLookDTO()
            {
                Id=x.Id,
                AdTitle = x.AdTitle,
                Price = x.Price,
                ImageData = x.Picture

            }).ToList();

            var count = ads.Count();
            if (count % 2 == 0)
                return ads;

            else
            {
                Random random = new Random();
                int randomIndex = random.Next(0, count); // Generate a random index within the list bounds

                ads.RemoveAt(randomIndex);
                return ads;
            }


           




        }

        public ProfileDTO GetProfile(string user)
        {
            var handler = new JwtSecurityTokenHandler();
            //trying
            //var token = handler.ReadJwtToken(user.Replace("\0", "")); //as JwtSecurityToken;
            var jsonToken = handler.ReadToken(user) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            // var userId = claims.FirstOrDefault(c => c.Type == "Email")?.Value;


            // extract the user from the cookie using email in the cookie
            var email_from_cookie = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

            var result = _context.tbl_user_details.Where(a=>a.Email==email_from_cookie).FirstOrDefault();
            var profile = new ProfileDTO
            {
                Name = result.Name,
                Email = result.Email,
                Address = result.Address,
                PhoneNumber = result.PhoneNumber,
            };
            return profile;
            




            
        }

        public ProfileDTO EditProfile(ProfileDTO profile, string user)
        {
            var handler = new JwtSecurityTokenHandler();
            //trying
            //var token = handler.ReadJwtToken(user.Replace("\0", "")); //as JwtSecurityToken;
            var jsonToken = handler.ReadToken(user) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            // var userId = claims.FirstOrDefault(c => c.Type == "Email")?.Value;


            // extract the user from the cookie using email in the cookie
            var email_from_cookie = claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            var result = _context.tbl_user_details.Where(a => a.Email == email_from_cookie).FirstOrDefault();
            result.Name=profile.Name;
            result.Email=profile.Email;
            result.Address=profile.Address;
            result.PhoneNumber=profile.PhoneNumber;




            _context.Update(result);
            _context.SaveChanges();


            var result1 = _userManager.FindByEmailAsync(email_from_cookie).Result;
            result1.Email=profile.Email;
            _userManager.UpdateAsync(result1);

            _context.SaveChanges();
            return profile;







        }

        public bool DeleteUser(string user)
        {
            var handler = new JwtSecurityTokenHandler();
            //trying
            //var token = handler.ReadJwtToken(user.Replace("\0", "")); //as JwtSecurityToken;
            var jsonToken = handler.ReadToken(user) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            // var userId = claims.FirstOrDefault(c => c.Type == "Email")?.Value;


            // extract the user from the cookie using email in the cookie
            var email_from_cookie = claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            var id_cookie = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var result = _context.tbl_user_details.Where(a => a.Email == email_from_cookie).FirstOrDefault();
            _context.tbl_user_details.Remove(result);


            var user1 = _userManager.FindByEmailAsync(email_from_cookie).Result;
           var result1= _userManager.DeleteAsync(user1).Result;
            // var exitStatus = Convert.ToBoolean(_context.SaveChanges())?true : false;
            _context.SaveChanges();
            var exitStatus = true;
            return exitStatus;

            
        }

        public ICollection<AdsFirstLookDTO> GetMyAds(string user)
        {
            var handler = new JwtSecurityTokenHandler();
            //trying
            //var token = handler.ReadJwtToken(user.Replace("\0", "")); //as JwtSecurityToken;
            var jsonToken = handler.ReadToken(user) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            // var userId = claims.FirstOrDefault(c => c.Type == "Email")?.Value;


            // extract the user from the cookie using email in the cookie
            var email_from_cookie = claims.FirstOrDefault(c => c.Type == "Email")?.Value;
           // var id_cookie = claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var result=_context.tbl_user_details.Where(a=>a.Email== email_from_cookie).FirstOrDefault();

            var ads=_context.tbl_ads.Where(a=>a.Ad_by_user.Id==result.Id).Select(x => new AdsFirstLookDTO()
            {
                Id = x.Id,
                AdTitle = x.AdTitle,
                Price = x.Price,
                ImageData = x.Picture

            }).ToList();

            return ads;





        }

        public ICollection<AdsFirstLookDTO> GetAdsByLocation(Location location, string user)
        {
            var handler = new JwtSecurityTokenHandler();
            //trying
            //var token = handler.ReadJwtToken(user.Replace("\0", "")); //as JwtSecurityToken;
            var jsonToken = handler.ReadToken(user) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            // var userId = claims.FirstOrDefault(c => c.Type == "Email")?.Value;


            // extract the user from the cookie using email in the cookie
            var email_from_cookie = claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            var ads = _context.tbl_ads.Where(x => x.Ad_Location.City == location.City && x.Ad_by_user.Email!=email_from_cookie).OrderBy(r => Guid.NewGuid()).Take(10).Select(x => new AdsFirstLookDTO()
            {
                Id = x.Id,
                AdTitle = x.AdTitle,
                Price = x.Price,
                ImageData = x.Picture

            }).ToList();   // k gareko vanda, database bata location same vako lai fetch gareko, ani random order ma rakheko guid use garera ani top 10 ligeko  ani mapping gareko;

            var count = ads.Count();

           


            if (ads == null || count == 1)
            { // yedi null xa vane random location bata ads fetch garera return garne 




                var ads1 = _context.tbl_ads.Where(a => a.Ad_by_user.Email != email_from_cookie).OrderBy(r => Guid.NewGuid()).Take(10).Select(x => new AdsFirstLookDTO()
                {
                    Id = x.Id,
                    AdTitle = x.AdTitle,
                    Price = x.Price,
                    ImageData = x.Picture

                }).ToList();



                var count1 = ads1.Count();
                if (count1 % 2 == 0)
                    return ads1;

                else
                {
                    Random random = new Random();
                    int randomIndex = random.Next(0, count1); // Generate a random index within the list bounds

                    ads1.RemoveAt(randomIndex);
                    return ads1;
                }


            }



            if (count % 2 == 0)
                return ads;

            else
            {
                Random random = new Random();
                int randomIndex = random.Next(0, count); // Generate a random index within the list bounds

                ads.RemoveAt(randomIndex);
                return ads;
            }




        }

        public ICollection<AdsFirstLookDTO> GetAdsRandomly(string user)
        {

            var handler = new JwtSecurityTokenHandler();
            //trying
            //var token = handler.ReadJwtToken(user.Replace("\0", "")); //as JwtSecurityToken;
            var jsonToken = handler.ReadToken(user) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            // var userId = claims.FirstOrDefault(c => c.Type == "Email")?.Value;


            // extract the user from the cookie using email in the cookie
            var email_from_cookie = claims.FirstOrDefault(c => c.Type == "Email")?.Value;


            var ads = _context.tbl_ads.Where(a=>a.Ad_by_user.Email!=email_from_cookie).OrderBy(r => Guid.NewGuid()).Take(10).Select(x => new AdsFirstLookDTO()
            {
                Id = x.Id,
                AdTitle = x.AdTitle,
                Price = x.Price,
                ImageData = x.Picture

            }).ToList();

            var count = ads.Count();
            if (count % 2 == 0)
                return ads;

            else
            {
                Random random = new Random();
                int randomIndex = random.Next(0, count); // Generate a random index within the list bounds

                ads.RemoveAt(randomIndex);
                return ads;
            }

        }
    }
}

