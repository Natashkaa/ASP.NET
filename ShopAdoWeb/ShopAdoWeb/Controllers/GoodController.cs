using ShopAdoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAdoWeb.Controllers
{
    public class GoodController : Controller
    {
        // GET: Good
        public ActionResult ShowGoods()
        {
            GoodRepository goodDB = new GoodRepository();
            return View(goodDB.GetAll());
        }
    }
}