using Hamro_Pasal.DTO;
using Hamro_Pasal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hamro_Pasal.Controllers
{
    public class PostAdsController : Controller
    {
        private readonly IPostAdsInterface _Ads;

        public PostAdsController(IPostAdsInterface ads)
        {
            _Ads = ads;
        }


        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public IActionResult PostAd(PostAdsDTO adsDetails)
        {
            var user = Request.Cookies["User"];
            if (user == null)
            {
                return RedirectToAction(actionName: "Signin", controllerName: "Register");

            }

            var result=_Ads.PostAds(adsDetails,user);






            return View();
        }
    }
}
