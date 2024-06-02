using Hamro_Pasal.Data;
using Hamro_Pasal.DTO;
using Hamro_Pasal.Interfaces;
using Hamro_Pasal.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace Hamro_Pasal.Repositories
{
    public class PostAdsRepository : IPostAdsInterface
    {

        private ApplicationDbContext _context;

        public PostAdsRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<UserManagerResponse> PostAds(PostAdsDTO postDetails,string user)
        {
            var postCategory = _context.tbl_category.Where(a => a.CategoryName == postDetails.category).FirstOrDefault();


            var PostLocation = _context.tbl_location.Where(a =>  a.City == postDetails.location.City).FirstOrDefault();


            if (postDetails.ImageFile != null && postDetails.ImageFile.Length > 0)
            {
                byte[] imageData;

                using (var memoryStream = new MemoryStream())
                {
                    postDetails.ImageFile.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }



                var post = new UserAds
                {
                    AdTitle = postDetails.AdTitle,
                    AdDescription = postDetails.AdDescription,
                    Price = postDetails.Price,
                    Category_ad = postCategory,
                    CreatedDate = DateTime.Now,
                    AdAddress = postDetails.AdAddress,
                    Picture = imageData,
                    //  Ad_Location = PostLocation;


                };
                var handler = new JwtSecurityTokenHandler();
                //trying
                //var token = handler.ReadJwtToken(user.Replace("\0", "")); //as JwtSecurityToken;
                var jsonToken = handler.ReadToken(user) as JwtSecurityToken;
                var claims = jsonToken.Claims;
                // var userId = claims.FirstOrDefault(c => c.Type == "Email")?.Value;


                // extract the user from the cookie using email in the cookie
                var email_from_cookie = claims.FirstOrDefault(c => c.Type == "Email")?.Value;
                var adsBy = _context.tbl_user_details.Where(a => a.Email == email_from_cookie).FirstOrDefault();

                post.Ad_by_user = adsBy;


                // var adsLocation=_context.tbl_location.Where(a=>a.City==postDetails.location.City && a.State==postDetails.location.State /*&& a.Neighbourhood==postDetails.location.Neighbourhood*/).FirstOrDefault();

                post.Ad_Location = PostLocation;

                _context.tbl_ads.Add(post);
                _context.SaveChanges();

                return new UserManagerResponse
                {
                    IsSuccess = true,
                    Message = "success"


                };
            }

            return new UserManagerResponse
            {
                IsSuccess = false,
                Message = "Failed to save the image"


            };








        }

        public bool save()
        {
            var result=_context.SaveChanges();
            throw new NotImplementedException();
        }
    }
}
