using BussinesLogic;
using SharedModels.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

#pragma warning disable CS0168


namespace EvidencijaPotrosnje.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        public ActionResult Index()
        {
            try
            {
                var wDir = Directory.CreateDirectory(HostingEnvironment.MapPath($"~/App_Data/WeatherData/"));
                var wFiles = wDir.GetFiles().ToList().Select(x => x.Name).ToList();
                List<String> weatFiles = new List<string>();
                foreach (var wf in wFiles)
                {
                    weatFiles.Add(wf.Split('.')[0].Split('-')[1]);
                }

                var cDir = Directory.CreateDirectory(HostingEnvironment.MapPath($"~/App_Data/ConsumptionData/"));
                var cFiles = cDir.GetFiles().ToList().Select(x => x.Name).ToList();
                List<String> consFiles = new List<string>();

                foreach (var cf in cFiles)
                {
                    consFiles.Add(cf.Split('.')[0]);
                }

                ViewBag.wFiles = weatFiles;
                ViewBag.cFiles = consFiles;
                ViewBag.ErrorMessage = TempData["ErrorMessage"];

                return View();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "Nesto neocekivano se desilo!";

                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Import(string[] weatherFile, string stateName, DateTime startDate, DateTime endDate) 
        {
            IImportData importData = new ImportData();

            try
            {
                if (weatherFile.Length == 0 || string.IsNullOrEmpty(stateName))
                    throw new Exception("Pick weather and consumption file first");
                if(startDate == null || endDate == null)
                    throw new Exception("Choose start and end date!");
                
                foreach(string singleFile in weatherFile)
                {
                    ImportParameters parametersWeather = new ImportParameters(HostingEnvironment.MapPath($"~/App_Data/WeatherData/Weather-{singleFile}.csv"),
                        null,
                        stateName, startDate, endDate);
                    importData.Load(parametersWeather);

                }

                ImportParameters parameters = new ImportParameters(null,
                        HostingEnvironment.MapPath($"~/App_Data/ConsumptionData/{stateName}.csv"),
                        stateName, startDate, endDate);
                importData.Load(parameters);

                HttpContext.Application.Remove("init");

                return RedirectToAction("Index", "Home");

            }
            catch ( Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}