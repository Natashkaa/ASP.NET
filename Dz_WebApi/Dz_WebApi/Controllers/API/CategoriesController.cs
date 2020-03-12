using ShopAdoDAL;
using ShopAdoDAL.Context;
using ShopAdoDAL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Dz_WebApi.Controllers.API
{
    public class CategoriesController : ApiController
    {
        private ShopAdoContext db = new ShopAdoContext();

        public CategoriesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        //GET api/Categories
        public HttpResponseMessage GetAllCategories()
        {
            IEnumerable<Category> allCategories = db.Category;
            if (allCategories == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Category DB is empty");
            }
            return Request.CreateResponse(HttpStatusCode.OK, allCategories);
        }

        // POST: api/Categories/5
        public HttpResponseMessage GetCategory(int id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Requested category with id {id} doesn’t exist in database");
            }

            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        // PUT: api/Categories/5
        public HttpResponseMessage EditCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != category.CategoryId)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ids does not match");
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Category with id {id} does not exist in database");
                }
                else { throw; }
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // POST: api/Categories
        public HttpResponseMessage AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            db.Category.Add(category);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        // DELETE: api/Categories/5
        public HttpResponseMessage DeleteCategory(int id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Requested category with id {id} doesn’t exist in database");
            }

            db.Category.Remove(category);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, category);
        }

        //POST: api/Categories/Manufacturer/1
        [ActionName("Manufacturer")]
        public IEnumerable<Category> GetCategoryByManufacturerId(int id)
        {
            IEnumerable<Category> category = from c in db.Category
                                             from g in db.Good
                                             where c.CategoryId == g.CategoryId
                                             where g.ManufacturerId == id
                                             select c;

            return category;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Category.Count(e => e.CategoryId == id) > 0;
        }
    }
}
