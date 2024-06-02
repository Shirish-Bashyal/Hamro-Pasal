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
            var user = Request.Cookies["User"];
             if(user==null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Register");

            }
            return View();
        }

        public IActionResult PostAd()
        {
            var user = Request.Cookies["User"];
             if(user==null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Register");

            }
            return View();
        }





        [HttpPost]
        public IActionResult PostAd(PostAdsDTO adsDetails)
        {
            var user = Request.Cookies["User"];
             if(user==null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Register");

            }



            var result = _Ads.PostAds(adsDetails, user).Result;








            //  return View();
            if (result.IsSuccess == true)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
                TempData["success"] = "Ad is Posted";
            }


            else
            {
                return View();
                TempData["error"] = "error occured";


            }
        }
    }
}
