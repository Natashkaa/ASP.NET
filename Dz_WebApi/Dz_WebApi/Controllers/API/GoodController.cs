using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ShopAdoDAL;
using ShopAdoDAL.Context;
using ShopAdoDAL.Entity;

namespace Dz_WebApi.Controllers.API
{
    public class GoodController : ApiController
    {
        private ShopAdoContext db = new ShopAdoContext();

        public GoodController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Good
        public HttpResponseMessage GetAllGoods()
        {
            IEnumerable<Good> allGoods = db.Good;
            if (allGoods == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Good DB is empty");
            }
            return Request.CreateResponse(HttpStatusCode.OK, allGoods);
        }

        // GET: api/Good/5
        public HttpResponseMessage GetGood(int id)
        {
            Good good = db.Good.Find(id);
            if (good == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Requested good with id {id} doesn’t exist in database");
            }

            return Request.CreateResponse(HttpStatusCode.OK, good);
        }

        // PUT: api/Good/5
        public HttpResponseMessage PutGood(int id, Good good)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != good.GoodId)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ids does not match");
            }

            db.Entry(good).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodExists(id)) {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Good with id {id} does not exist in database");
                }
                else { throw; }
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // POST: api/Good
        public HttpResponseMessage PostGood(Good good)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.Good.Add(good);
            db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = good.GoodId }, good);
            return Request.CreateResponse(HttpStatusCode.OK, good);
        }

        // DELETE: api/Good/5
        public HttpResponseMessage DeleteGood(int id)
        {
            Good good = db.Good.Find(id);
            if (good == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Requested good with id {id} doesn’t exist in database");
            }

            db.Good.Remove(good);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, good);
        }

        //POST: api/Good/Categories/1
        [ActionName("Categories")]
        public IEnumerable<Good> Categories(int id)
        {
            IEnumerable<Good> goods = from x in db.Good
                                      where x.CategoryId == id
                                      select x;
            return goods;
        }
        
        //POST: api/Good/Manufacturers/1
        [ActionName("Manufacturers")]
        public IEnumerable<Good> Manufacturers(int id)
        {
            IEnumerable<Good> goods = from x in db.Good
                                      where x.ManufacturerId == id
                                      select x;

            return goods;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoodExists(int id)
        {
            return db.Good.Count(e => e.GoodId == id) > 0;
        }
    }
}