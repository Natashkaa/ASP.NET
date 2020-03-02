using Shop.DAL;
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
        IRepository<Good> goodRepo;
        IRepository<Category> categoryRepo;
        IRepository<Manufacturer> manufacturerRepo;
        IRepository<Photo> photoRepo;

        const int Goods_per_page = 6;

        public HomeController(IRepository<Good> goodRep,
                              IRepository<Category> catRep, 
                              IRepository<Manufacturer> manufRep, 
                              IRepository<Photo> photoRep)
        {
            this.goodRepo = goodRep;
            this.categoryRepo = catRep;
            this.manufacturerRepo = manufRep;
            this.photoRepo = photoRep;

            ViewBag.categoryList = new SelectList(categoryRepo.GetAll().ToList(), "CategoryId", "CategoryName");
            ViewBag.manufacturerList = new SelectList(manufacturerRepo.GetAll().ToList(), "ManufacturerId", "ManufacturerName");
            ViewBag.photoLink = photoRepo.Get(2).PhotoPath;
        }
        // GET: Good
        public ActionResult ShowGoods(int id = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)goodRepo.GetAll().Count() / Goods_per_page);
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
            var good = goodRepo.GetAll().ToList().Find(x => x.GoodId == id);
            goodRepo.Delete(good);
            goodRepo.Save();
            return RedirectToAction("ShowGoods");
        }

        public ActionResult GoodTable(int id = 1)
        {
            var goods = goodRepo
                        .GetAll()
                         .ToList()
                          .Skip((id - 1) * Goods_per_page)
                           .Take(Goods_per_page);
            ViewBag.Goods = goods;
            var photochki = photoRepo.GetAll();
            foreach (Good g in goods)
            {
                g.PhotoCollection = new List<string>();
                IEnumerable<Photo> test = from s in photochki
                                          where s.GoodId == g.GoodId
                                          select  s;
                if (test.Count() > 0)
                {
                    foreach (Photo l in test)
                    {
                        g.PhotoCollection.Add(l.PhotoPath);
                    }
                }
                else { g.PhotoCollection.Add("Source/unknown.jpg"); }
                
            }
            return PartialView(goods);
        }

        public ActionResult EditGood(int id = 0)
        {
            if (id == 0) return View(new GoodVM());

            var good = goodRepo.Get(id);
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
        public ActionResult EditGood(GoodVM editedGood)
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
                goodRepo.CreateOrUpdate(good);
            }
            return RedirectToAction("ShowGoods");
        }

        public ActionResult CreateGood()
        {
            GoodVM newGood = new GoodVM();
            return View(newGood);
        }

        [HttpPost]
        public ActionResult CreateGood(GoodVM newGood, IEnumerable<HttpPostedFileBase> fileUpload)
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
                goodRepo.CreateOrUpdate(good);
            }
            return RedirectToAction("ShowGoods");
        }
    }
}