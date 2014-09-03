using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApplication.Models;
using WeatherApplication.ViewModels;

namespace WeatherApplication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Index([Bind(Include = "ScreenName")] HomeIndexViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var service = new WeatherService();
                    model.Geoname = service.FindLocations(model.ScreenName);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View("Index", model);
        }
           public ActionResult Weather([Bind(Include = "Name, Country, County")]Geoname geoname, string screenName, int locationID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = new HomeIndexViewModel();
                    var service = new WeatherService();
                     model.Weathers = service.FindWeathers(geoname, screenName, locationID);
                    

                    return View("Weather", model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View("Error");
        }

    }
}

    

    


