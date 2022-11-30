using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEC.Models;
using System.Net.Http;

namespace NEC.Controllers
{
    public class mvcController : Controller
    {
        // GET: mvc
        public ActionResult Index()
        {
            IEnumerable<admin>local=null;
            HttpClient hc=new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44325/api/admin");
            var consumeapi = hc.GetAsync("admin");
            consumeapi.Wait();
            var readdata=consumeapi.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var displaydata=readdata.Content.ReadAsAsync<IList<admin>>();
                displaydata.Wait();
                local = displaydata.Result;
            }
            return View(local);
        }
       
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(admin insert)
        {
            HttpClient hc=new HttpClient();
            hc.BaseAddress= new Uri("https://localhost:44325/api/admin");
            var insertrecord = hc.PostAsJsonAsync<admin>("admin", insert);
            var savedata=insertrecord.Result;
            if(savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("create");
        }
        public ActionResult edit(int id)
        {
            admin ac = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44325/api/");
            var consumeapi=hc.GetAsync("admin?id="+id.ToString());
            consumeapi.Wait();
            var readdata = consumeapi.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var displaydata=readdata.Content.ReadAsAsync<admin>();
                displaydata.Wait();
                ac = displaydata.Result;
            }
            return View(ac);
        }
        [HttpPost]
        public ActionResult edit(admin ae)
        {
            
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44325/api/admin");
            var insertrecord = hc.PutAsJsonAsync<admin>("admin", ae);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "not updated";
            }
            return View(ae);

        }
        public ActionResult Delete(int id)
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44325/api/admin");
            var delrecord = hc.DeleteAsync("admin/" + id.ToString());
            delrecord.Wait();
            var displaydata=delrecord.Result;
            if(displaydata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}