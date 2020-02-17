using ShopAdoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAdoWeb.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult ShowCategory()
        {
            CategoryRepository categoryDB = new CategoryRepository();
            return View(categoryDB.GetAll());
        }
    }
}