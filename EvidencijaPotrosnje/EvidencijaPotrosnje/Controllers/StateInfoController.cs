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
            ViewBag.StateError = TempData["StateError"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(string StateName)
        {
            try
            {
                if (string.IsNullOrEmpty(StateName))
                    throw new Exception("Ime drzave ne moze biti prazno");

                DBLogic dBLogic = new DBLogic();

                StateInfoModel state = new StateInfoModel ();
                state.StateName = StateName;

                dBLogic.AddState(state);
                return View();
            }
            catch (Exception e)
            {
                TempData["StateError"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}