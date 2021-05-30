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
            
            ImportParameters parameters = new ImportParameters(HostingEnvironment.MapPath("~/App_Data/WeatherData/Weather-Serbia.csv"), HostingEnvironment.MapPath("~/App_Data/ConsumptionData/Consumption.csv"), "Serbia", DateTime.MinValue, DateTime.MaxValue);

            ImportData.Load(parameters);



            return View(CurrentData.Data);
        }

        public ActionResult Import(string weatherFile, string stateName, DateTime startDate, DateTime endDate)
        {

            ImportParameters parameters = new ImportParameters(HostingEnvironment.MapPath($"~/App_Data/WeatherData/Weather-{weatherFile}.csv"), HostingEnvironment.MapPath("~/App_Data/ConsumptionData/Consumption.csv"), stateName, startDate, endDate);

            ImportData.Load(parameters);



            return View(CurrentData.Data);
        }
    }
}