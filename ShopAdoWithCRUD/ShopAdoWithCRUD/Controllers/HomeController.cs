using Shop_BLL;
using Shop_DAL;
using Shop_DAL.Context;
using Shop_DAL.Repository;
using Shop_DAL.UnitOfWork;
using ShopAdoWithCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopAdoWithCRUD.Controllers
{
    public class HomeController : Controller
    {
        GoodRepository goodDB = new GoodRepository();
        CategoryRepository categoryRepo = new CategoryRepository();
        ManufacturerRepository manufacturerRepo = new ManufacturerRepository();
        const int Goods_per_page = 6;

        public HomeController()
        {
            ViewBag.categoryList = new SelectList(categoryRepo.GetAll().ToList(), "CategoryId", "CategoryName");
            ViewBag.manufacturerList = new SelectList(manufacturerRepo.GetAll().ToList(), "ManufacturerId", "ManufacturerName");
        }
        // GET: Good
        public ActionResult ShowGoods(int id = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)goodDB.GetAll().Count() / Goods_per_page);
            if (id > (int)ViewBag.PageCount)
            {
                id = (int)ViewBag.PageCount;
            }
            else if (id == 0)
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

        public ActionResult EditGood(int id = 0)
        {
            if (id == 0) return View(new GoodVM());

            var good = goodDB.Get(id);
            var goodForEdit = new GoodVM
            {
                Id = good.GoodId,
                GoodName = good.GoodName,
                Price = good.Price,
                Count = good.GoodCount
            };

            return View(goodForEdit);
        }

        [HttpPost]
        public ActionResult Edit(GoodVM editedGood)
        {
            if (ModelState.IsValid)
            {
                Good good = new Good
                {
                    GoodId = editedGood.Id,
                    GoodName = editedGood.GoodName,
                    Price = editedGood.Price,
                    GoodCount = editedGood.Count,
                    CategoryId = editedGood.CategoryId,
                    ManufacturerId = editedGood.ManufacturerId
                };
                goodDB.CreateOrUpdate(good);
            }
            return RedirectToAction("ShowGoods");
        }

        public ActionResult CreateGood()
        {
            GoodVM newGood = new GoodVM();
            return View(newGood);
        }

        [HttpPost]
        public ActionResult CreateGood(GoodVM newGood)
        {
            if (ModelState.IsValid)
            {
                Good good = new Good
                {
                    GoodName = newGood.GoodName,
                    GoodCount = newGood.Count,
                    Price = newGood.Price,
                    CategoryId = newGood.CategoryId,
                    ManufacturerId = newGood.ManufacturerId
                };
                goodDB.CreateOrUpdate(good);
            }
            return RedirectToAction("ShowGoods");
        }
    }
}