using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BussinesLogic;
using SharedModels;

namespace EvidencijaPotrosnje.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            StateWeatherModel swm = new StateWeatherModel()
            {
                AirTemperature = 12,
                CloudCover = "aaaa",
                DevpointTemperature = 34,
                GustValue = 34,
                HorizontalVisibility = 44,
                Humidity = 33,
                PresentWeather = "aaaa",
                RecentWeather = "wwww",
                ReducedPressure = 44,
                StationPressure = 22,
                WindDirection = "aaaa",
                WindSpeed = 44,
                LocalTime = DateTime.Now
            };

            StateConsumptionModel scm = new StateConsumptionModel()
            {
                CovRatio = 33,
                DateFrom = DateTime.Now,
                DateShort = DateTime.Now,
                DateTo = DateTime.Now,
                DateUTC = DateTime.Now,
                StateCode = "aaa",
                Value = 44,
                ValueScale = 55
            };

            StateInfoModel sim = new StateInfoModel();
            sim.StateConsumption = scm;
            sim.StateWeather = swm;
            sim.StateName = "qwerty";

            DBLogic.RemoveAllStates();
            DBLogic.AddOrUpdateState(sim);

            sim.StateWeather.AirTemperature = 445;
            DBLogic.AddOrUpdateState(sim);

            return View();
        }
    }
}