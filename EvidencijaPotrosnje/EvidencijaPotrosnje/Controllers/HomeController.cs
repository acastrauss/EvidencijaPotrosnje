using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BussinesLogic;
using SharedModels;
using SharedModels.HelperClasses;
using System.Web.Hosting;

namespace EvidencijaPotrosnje.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //DBLogic.RemoveAllStates();

            List<StateInfoModel> states = (List<StateInfoModel>) DBLogic.GetAllStates();
            ViewBag.Models = states;

            return View(states);
        }

        public ActionResult Import(string weatherFile, string stateName, DateTime startDate, DateTime endDate)
        {

            ImportParameters parameters = new ImportParameters(HostingEnvironment.MapPath($"~/App_Data/WeatherData/Weather-{weatherFile}.csv"), "", stateName, startDate, endDate);
            //ImportParameters parameters = new ImportParameters(HostingEnvironment.MapPath("~/App_Data/WeatherData/Weather-Serbia.csv"), HostingEnvironment.MapPath("~/App_Data/ConsumptionData/Consumption.csv"), "Srbija", DateTime.MinValue, DateTime.MaxValue);
            ImportData.Load(parameters);

            StateInfoModel state = DBLogic.GetStateByName(stateName);

            return RedirectToAction("Index");
        }
    }
}