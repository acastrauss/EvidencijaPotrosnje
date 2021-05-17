using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinesLogic;
using SharedModels;

namespace EvidencijaPotrosnje.Controllers
{
    public class StateInfoController : Controller
    {
        // GET: StateInfo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string StateName)
        {
            StateConsumptionModel consumptionModel = new StateConsumptionModel(1, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, "SPA", 1, 1);
            StateWeatherModel weatherModel = new StateWeatherModel(1, "1%", 1, 1, 1, 1, "weather", "weather", 1, 1, "N", 1, DateTime.Now);
            StateInfoModel state = new StateInfoModel { StateName = StateName, StateConsumption = consumptionModel, StateWeather = weatherModel };

            DBLogic.AddOrUpdateState(state);
            return View();
        }
    }
}