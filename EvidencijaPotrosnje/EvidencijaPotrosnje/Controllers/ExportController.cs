using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinesLogic;
using SharedModels.HelperClasses;

namespace EvidencijaPotrosnje.Controllers
{
    public class ExportController : Controller
    {
        // GET: Export
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Export(string[] columns)
        {
            List<ShowingData> statesData = (List<ShowingData>)HttpContext.Application["showingData"];
            //ExportData.SaveData(statesData, columns);
            return RedirectToAction("Index");
        }
    }
}