using EvidencijaPotrosnje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EvidencijaPotrosnje.Controllers
{
    public class DrzavaController : Controller
    {
        // GET: Drzava
        public ActionResult Index()
        {
            ResProbaEntities DB = new ResProbaEntities();
            List<Drzava> drzave = DB.Drzavas.ToList();
            return View(drzave);
        }
    }
}