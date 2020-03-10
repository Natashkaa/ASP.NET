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
        // GET: api/Goods
        public IEnumerable<Good> GetGood()
        {
            return db.Good.ToList();
        }

        // GET: api/Goods/5
        [ResponseType(typeof(Good))]
        public IHttpActionResult GetGood(int id)
        {
            Good good = db.Good.Find(id);
            if (good == null)
            {
                return NotFound();
            }

            return Ok(good);
        }

        // PUT: api/Goods/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGood(int id, Good good)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != good.GoodId)
            {
                return BadRequest();
            }

            db.Entry(good).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Goods
        [ResponseType(typeof(Good))]
        public IHttpActionResult PostGood(Good good)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Good.Add(good);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = good.GoodId }, good);
        }

        // DELETE: api/Goods/5
        [ResponseType(typeof(Good))]
        public IHttpActionResult DeleteGood(int id)
        {
            Good good = db.Good.Find(id);
            if (good == null)
            {
                return NotFound();
            }

            db.Good.Remove(good);
            db.SaveChanges();

            return Ok(good);
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