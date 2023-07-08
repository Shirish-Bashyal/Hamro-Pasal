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

            //var result = _homeInterface.GetAdsRandomly();
            //return View(result);
            return View();
        }


        public IActionResult Index1(LocationDTO location)
        {


            var mapped=_mapper.Map<Location>(location);
            var result = _homeInterface.GetAdsByLocation(mapped);


          

           
            return View(result);
        }
    }
}
