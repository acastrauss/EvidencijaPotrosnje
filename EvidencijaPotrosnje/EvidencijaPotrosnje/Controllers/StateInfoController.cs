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
            DBLogic dBLogic = new DBLogic();

            StateInfoModel state = new StateInfoModel ();
            state.StateName = StateName;

            dBLogic.AddState(state);
            return View();
        }
    }
}