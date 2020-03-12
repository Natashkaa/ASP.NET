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

namespace Dz_WebApi.Controllers.API
{
    public class ManufacturerController : ApiController
    {
        private ShopAdoContext db = new ShopAdoContext();
        public ManufacturerController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Manufacturer
        public HttpResponseMessage GetAllManufacturers()
        {
            IEnumerable<Manufacturer> allManufacturers = db.Manufacturer;
            if (allManufacturers == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Manufacturer DB is empty");
            }
            return Request.CreateResponse(HttpStatusCode.OK, allManufacturers);
        }

        // GET: api/Manufacturer/5
        public HttpResponseMessage GetManufacturer(int id)
        {
            Manufacturer manufacturer = db.Manufacturer.Find(id);
            if (manufacturer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Requested manufacturer with id {id} doesn’t exist in database");
            }

            return Request.CreateResponse(HttpStatusCode.OK, manufacturer);
        }

        // PUT: api/Manufacturer/5
        public HttpResponseMessage EditManufacturer(int id, Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != manufacturer.ManufacturerId)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Ids does not match");
            }

            db.Entry(manufacturer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Manufacturer with id {id} does not exist in database");
                }
                else { throw; }
            }
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // POST: api/Manufacturer
        public HttpResponseMessage AddManufacturer(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            db.Manufacturer.Add(manufacturer);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, manufacturer);
        }

        // DELETE: api/Manufacturer/5
        public HttpResponseMessage DeleteManufacturer(int id)
        {
            Manufacturer manufacturer = db.Manufacturer.Find(id);
            if (manufacturer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Requested manufacturer with id {id} doesn’t exist in database");
            }

            db.Manufacturer.Remove(manufacturer);
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, manufacturer);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ManufacturerExists(int id)
        {
            return db.Manufacturer.Count(e => e.ManufacturerId == id) > 0;
        }
    }
}