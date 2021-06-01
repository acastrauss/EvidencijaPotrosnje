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

            if (HttpContext.Application["init"] == null)
                this.InitApp();
            
            return View();
        }

        private void InitApp() 
        {
            DBLogic dBLogic = new DBLogic();
            IMapCW mapCW = new MapCW();

            HttpContext.Application["init"] = true;

            List<StateInfoModel> states = (List<StateInfoModel>)dBLogic.GetAllStates();

            List<ShowingData> showingData = new List<ShowingData>();

            foreach (var state in states)
            {
                showingData.AddRange(mapCW.MapData(state));
            }

            HttpContext.Application["showingData"] = showingData;
        }

        public ActionResult Import()
        {
            return RedirectToAction("Index", "Import");
        }

        [HttpPost]
        public ActionResult FilterDataName(String stateName) 
        {
            IDataManipulation dataManipulation = new DataManipulation();

            var list = (List<ShowingData>)HttpContext.Application["showingData"];
            HttpContext.Application["showingData"] = new List<ShowingData>();
            HttpContext.Application["showingData"] = dataManipulation.FilterByName(stateName, list);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult FilterDataDate(DateTime startDate, DateTime endDate) 
        {
            IDataManipulation dataManipulation = new DataManipulation();

            var list = (List<ShowingData>)HttpContext.Application["showingData"];
            HttpContext.Application["showingData"] = new List<ShowingData>();
            HttpContext.Application["showingData"] = dataManipulation.FilterByTime(startDate, endDate, list);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DiscardFilters() 
        {
            this.InitApp();
            return RedirectToAction("Index", "Home");
        }
    }
}