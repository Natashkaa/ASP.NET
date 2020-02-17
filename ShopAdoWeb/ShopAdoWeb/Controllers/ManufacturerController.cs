using ShopAdoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAdoWeb.Controllers
{
    public class ManufacturerController : Controller
    {
        // GET: Manufacturer
        public ActionResult ShowManufacturer()
        {
            ManufacturerRepository manDB = new ManufacturerRepository();
            return View(manDB.GetAll());
        }
    }
}