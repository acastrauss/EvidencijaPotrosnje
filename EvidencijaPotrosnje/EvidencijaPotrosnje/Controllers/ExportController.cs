using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
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
            ViewBag.ExportError = TempData["ExportError"];
            return View();
        }

        [HttpPost]
        public ActionResult Export(string[] columns)
        {
            try
            {
                List<ShowingData> statesData = (List<ShowingData>)HttpContext.Application["showingData"];
                IExportData export = new ExportData();
                string fullPath = export.SaveData(statesData, columns);

                string fileName = fullPath.Substring(fullPath.LastIndexOf("\\") + 1);
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/csv";
                response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                response.TransmitFile(fullPath);
                response.Flush();
                response.End();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ExportError"] = e.Message;
                return RedirectToAction("Index");
            }
            
        }
    }
}