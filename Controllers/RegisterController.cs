﻿using Hamro_Pasal.DTO;
using Hamro_Pasal.Interfaces;
using Hamro_Pasal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hamro_Pasal.Controllers
{
    public class RegisterController : Controller
    {

        private IUserInterface _userInterface;
        public RegisterController(IUserInterface userInterface)
        {
            _userInterface = userInterface;

        }
        public IActionResult Index()
        {
            var user = Request.Cookies["User"];
            if (user != null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");

            }
            return View();
        }


        public IActionResult Register()
        {
           


            return View();
        }



        [HttpPost]
        public IActionResult Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {

                //if(model.Password!=model.ConfirmPassword)
                //{
                //    ViewBag.Message = "Password and ConfirmPassword Doesnot match";
                //    return View(model);
                //}

                
               
                
               var result= _userInterface.RegisterUserAsync(model).Result;//.result returns the response of the function
                // RedirectToAction(SiginIn);
                if (result.IsSuccess == true)
                {

                    ViewBag.Message=result.Message;
                    return RedirectToAction(actionName: "Signin", controllerName: "Register");

                }

                ViewBag.Message = result.Message;
                return View();
            }



            return View();
        }


        [HttpGet]

        public IActionResult Signin()

        {

            SigninDTO _Model = new SigninDTO();

            return View(_Model);

        }



        [HttpPost]
        public IActionResult Signin(SigninDTO model)
        {
            var result = _userInterface.SignInUserAsync(model).Result;
            if (result.IsSuccess != true)
            {
                ViewBag.Message = result.Message;
                return View();
            }


            var cookieOptions = new CookieOptions();

            cookieOptions.Expires = DateTime.Now.AddDays(30);
            cookieOptions.Path = "/";
            Response.Cookies.Append("User", result.Message, cookieOptions);
            //  Response.Cookies.Append("Role1", status.Role.ToString(), cookieOptions);

           
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            



        }




        public IActionResult signout()

        {
            //CookieOptions.Expires = DateTime.Now.AddSeconds(1);
            // Session.Abandon();
            //HttpContext.Current.Session.Abandon();
            //string v = Request.Cookies["UserId"];
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(-1d);


            Response.Cookies.Append("User", "null", cookieOptions);
            //var cookieOptions1 = new CookieOptions();
            //cookieOptions1.Expires = DateTime.Now.AddDays(-1d);

            //  Response.Cookies.Append("SomeCookie", "null", cookieOptions);
            //  var UserId = Int32.Parse(Request.Cookies["UserId"]);

            return RedirectToAction(actionName: "Index", controllerName: "Home");


        }






















    }

}








