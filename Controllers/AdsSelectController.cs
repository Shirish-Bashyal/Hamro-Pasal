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
            //var User = Request.Cookies["User"];
            //var token = JwtPayload.Deserialize(User);

            var result = _adsSelect.AdsDetails(id);






            return View(result);
        }

        public IActionResult EditAds(int id)
        {
            var result = _adsSelect.AdsDetails(id);

            return View(result);
        }

        [HttpPost]
        public IActionResult EditThisAds(int id)
        {
            return View();
        }

        
        public IActionResult DeleteThisAds(int id)
        {


            var result=_adsSelect.DeleteAd(id);
                return RedirectToAction(actionName: "GetMyAds", controllerName: "Home");

        }





    }
}
