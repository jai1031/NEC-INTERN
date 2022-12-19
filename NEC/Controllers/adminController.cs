using NEC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NEC.Controllers
{
    public class adminController : ApiController
    {
        TACV_DBEntities db = new TACV_DBEntities();
        public string post(admin product)
        {
            db.admins.Add(product);
            db.SaveChanges();
            return "succes";
        }
        public IEnumerable<admin> Get()
        {
            return db.admins.ToList();
        }
        public admin Get(int id)
        {
            admin product = db.admins.Find(id);
            return product;
        }
        public IHttpActionResult put( admin product)
        {
            var product_ = db.admins.Where(x => x.Id==product.Id).FirstOrDefault<admin>();
            if (product_ != null)
            {
                product_.CompanyName = product.CompanyName;
                product_.Event = product.Event;
                product_.Date = product.Date;
                product_.SSCcriteria = product.SSCcriteria;
                product_.HSCcriteria = product.HSCcriteria;
                product_.CGPA = product.CGPA;
                product_.Website = product.Website;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        public string delete(int id)
        {
            admin product = db.admins.Find(id);
            db.admins.Remove(product);
            db.SaveChanges();
            return "delete";
        }

    }
}
