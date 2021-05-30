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
            StateInfoModel state = new StateInfoModel ();

            DBLogic.AddOrUpdateState(state);
            return View();
        }
    }
}