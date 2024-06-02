using Hamro_Pasal.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Hamro_Pasal.Controllers
{
    public class AdsSelectController : Controller
    {
        private IAdsSelectInterface _adsSelect;
        public AdsSelectController(IAdsSelectInterface adsSelect)
        {
            _adsSelect = adsSelect;

        }
        public IActionResult Index(int id)
        {
            var User = Request.Cookies["User"];
            //var token = JwtPayload.Deserialize(User);

            if(User == null)
            {
                TempData["error"] = "Please LogIn";

                return RedirectToAction(actionName: "Index", controllerName: "Register");

            }

            var result = _adsSelect.AdsDetails(id);
            if(result == null)
            {
                TempData["error"] = "Some Error Occured";
                return View();
            }







            return View(result);
        }

        public IActionResult EditAds(int id)
        {
            var result = _adsSelect.AdsDetails(id);
            if (result != null)
            {


                return View(result);
            }

            TempData["error"] = "Some Error Occured";
            return View();


        }

        [HttpPost]
        public IActionResult EditThisAds(int id)
        {
            TempData["error"] = "Comming Soon";

            return View();
        }

        
        public IActionResult DeleteThisAds(int id)
        {


            var result=_adsSelect.DeleteAd(id);
            if (result == true)
            {
                TempData["success"] = "Ad Deleted";

                return RedirectToAction(actionName: "GetMyAds", controllerName: "Home");
            }

            TempData["error"] = "Some error Occured";

            return View();


        }





    }
}
