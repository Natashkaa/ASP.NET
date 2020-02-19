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
        GoodRepository goodDB = new GoodRepository();
        const int Goods_per_page = 6;
        // GET: Good
        public ActionResult ShowGoods(int id = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)goodDB.GetAll().Count() / Goods_per_page);
            if(id > (int)ViewBag.PageCount)
            {
                id = (int)ViewBag.PageCount;
            }
            else if(id == 0)
            {
                id = 1;
            }

            ViewBag.IncrDecr = id;
            return View();
        }
        
        public ActionResult Delete(int id)
        {

            var good = goodDB.GetAll().ToList().Find(x => x.GoodId == id);
            goodDB.Delete(good);
            goodDB.Save();
            return RedirectToAction("ShowGoods");
        }

        public ActionResult GoodTable(int id = 1)
        {
            var goods = goodDB
                        .GetAll()
                         .ToList()
                          .Skip((id - 1) * Goods_per_page)
                           .Take(Goods_per_page);
            ViewBag.Goods = goods;
            return PartialView(goods);
        }
    }
}