using AutoMapper;
using Hamro_Pasal.DTO;
using Hamro_Pasal.Interfaces;
using Hamro_Pasal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hamro_Pasal.Controllers
{

    public class HomeController : Controller
    {
        private IHomeInterface _homeInterface;
        private IMapper _mapper;

        public HomeController(IHomeInterface homeInterface, IMapper mapper)
        {
            _homeInterface = homeInterface;
            _mapper = mapper;


        }
        public IActionResult Index()// randomly ads select garne location select nahuda ani data fetch garne
        {
            var user = Request.Cookies["User"];
            if(user==null)
            {
                var result = _homeInterface.GetAdsRandomly();
                return View(result);
            }
            else
            {

                var result = _homeInterface.GetAdsRandomly(user);
              //  TempData["success"] = "cjhbjcjcb";

                return View(result);
            }



            // return View();
        }

        public IActionResult Location()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(LocationDTO location)
        {
            var user = Request.Cookies["User"];
            var mapped = _mapper.Map<Location>(location);


             if(user==null)
            {

                var result = _homeInterface.GetAdsByLocation(mapped);
                return View(result);

            }
            else
            {

                var result= _homeInterface.GetAdsByLocation(mapped,user);
                return View(result);

            }






        }


        [HttpGet]
        public IActionResult Profile()
        {
            var user = Request.Cookies["User"];
             if(user==null)
            {
                TempData["error"] = "Please LogIn";


                return RedirectToAction(actionName: "Index", controllerName: "Register");

            }


            var result =_homeInterface.GetProfile(user);
            if (result == null)
            {
                TempData["error"] = "Failed Due to Some Problem!!";
                return View(result);



            }


            return View(result);    
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            var user = Request.Cookies["User"];
             if(user==null)
            {
                TempData["error"] = "Please LogIn";


                return RedirectToAction(actionName: "Index", controllerName: "Register");

            }


            var result = _homeInterface.GetProfile(user);
            if (result == null)
            {
                TempData["error"] = "Failed Due to Some Problem!!";
                return View(result);



            }


            return View(result);
        }

        [HttpPost]
        public IActionResult EditProfile(ProfileDTO profile)
        {
            var user = Request.Cookies["User"];
             if(user==null)
            {
                TempData["error"] = "Please LogIn";


                return RedirectToAction(actionName: "Index", controllerName: "Register");

            }

            if (ModelState.IsValid)
            {
                var result = _homeInterface.EditProfile(profile, user);
                TempData["success"] = "Profile Edited";

                return RedirectToAction(actionName: "Profile", controllerName: "Home");

            }


            TempData["error"] = "Please provide relavant data";
                return RedirectToAction(actionName: "Profile", controllerName: "Home");



        }

        public IActionResult DeleteUser()
        {
            var user = Request.Cookies["User"];
             if(user==null)
            {
                TempData["error"] = "Please LogIn";


                return RedirectToAction(actionName: "Index", controllerName: "Register");

            }

            var result=_homeInterface.DeleteUser(user);
            if (result == true)
            {
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(-1d);


                Response.Cookies.Append("User", "null", cookieOptions);

                TempData["success"] = "User Deleted";

                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            TempData["error"] = "Some problem occured";

            return RedirectToAction(actionName: "Profile", controllerName: "Home");


        }
        [HttpGet]
        public IActionResult GetMyAds()
        {
            var user = Request.Cookies["User"];
             if(user==null)
            {
                TempData["error"] = "Please LogIn";


                return RedirectToAction(actionName: "Index", controllerName: "Register");

            }
            var result=_homeInterface.GetMyAds(user);
            return View(result);
        }




    }
}
