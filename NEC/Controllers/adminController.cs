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
        jaics1Entities db = new jaics1Entities();
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
            var product_ = db.admins.Where(x => x.id==product.id).FirstOrDefault<admin>();
            if (product_ != null)
            {
                product_.company_name = product.company_name;
                product_.@event = product.@event;
                product_.date = product.date;
                product_.C10th_criteria = product.C10th_criteria;
                product_.C12th_criteria = product.C12th_criteria;
                product_.CPGA = product.CPGA;
                product_.website = product.website;
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
