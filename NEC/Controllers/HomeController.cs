using NEC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NEC.Controllers
{
    public class HomeController : Controller
    {
        TACV_DBEntities1 entity = new TACV_DBEntities1();
        //TACV_DBEntities en = new TACV_DBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminLoginModel Credentials)
        {
            bool userexit = entity.AdminUserTables.Any(x => x.AdminUser == Credentials.Username && x.AdminPassword == Credentials.Password);
            AdminUserTable u = entity.AdminUserTables.FirstOrDefault(x => x.AdminUser == Credentials.Username && x.AdminPassword == Credentials.Password);
            if (userexit)
            {
                FormsAuthentication.SetAuthCookie(u.AdminUser, false);
                return RedirectToAction("Index","mvc");
                //return RedirectToAction();
            }
            ModelState.AddModelError("", "username or password wrong");
            return View();
        }
        


        public ActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentLogin(StudentLoginViewModel Credentials)
        {

            bool userexit = entity.UsersTbls.Any(x => x.Email == Credentials.Username && x.Passcode == Credentials.Password);
            UsersTbl u = entity.UsersTbls.FirstOrDefault(x => x.Email == Credentials.Username && x.Passcode == Credentials.Password);
            if (userexit)
            {
                FormsAuthentication.SetAuthCookie(u.Username, false);
                return RedirectToAction("UpdateProfile");
            }

            //ModelState.AddModelError("", "Username or Password is wrong");

            return View();
        }
        

        public ActionResult StudentIndex()
        {
            return View();
        }

        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult UpdateProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateProfile(StudentDetail tt, HttpPostedFileBase fileobj)
        {
            string con = ConfigurationManager.ConnectionStrings["TACV_DBEntities2"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "insert into StudentDetails (ID, Name, Department, Rollno, Semester, SSC, HSC, CGPA, Backlogs, PhoneNumber, EmailId, Photo, Resume, Address) values (@ID, @Name, @Department, @Rollno, @Semester, @SSC, @HSC, @CGPA, @Backlogs, @PhoneNumber, @EmailId, @Photo, @Resume, @Address)";
            SqlCommand com = new SqlCommand(query, sqlcon);
            sqlcon.Open();
            com.Parameters.AddWithValue("@ID", tt.ID);
            com.Parameters.AddWithValue("@Name", tt.Name);
            com.Parameters.AddWithValue("@Department", tt.Department);
            com.Parameters.AddWithValue("@Rollno", tt.Rollno);
            com.Parameters.AddWithValue("@Semester", tt.Semester);
            com.Parameters.AddWithValue("@SSC", tt.SSC);
            com.Parameters.AddWithValue("@HSC", tt.HSC);
            com.Parameters.AddWithValue("@CGPA", tt.CGPA);
            com.Parameters.AddWithValue("@Backlogs", tt.Backlogs);
            com.Parameters.AddWithValue("@PhoneNumber", tt.PhoneNumber);
            com.Parameters.AddWithValue("@EmailId", tt.EmailId);
            if (fileobj != null && fileobj.ContentLength > 0)
            {
                string filename = Path.GetFileName(fileobj.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/Files/"), filename);
                fileobj.SaveAs(imgpath);
            }
            com.Parameters.AddWithValue("@Photo", "~/Files/" + tt.Photo);
            if (fileobj != null && fileobj.ContentLength > 0)
            {
                string filename = Path.GetFileName(fileobj.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/Files/"), filename);
                fileobj.SaveAs(imgpath);
            }
            com.Parameters.AddWithValue("@Resume", "~/Files/" + tt.Resume);
            com.Parameters.AddWithValue("@Address", tt.Address);

            com.ExecuteNonQuery();
            sqlcon.Close();
            ViewData["Executed"] = tt.Name + " your details are updated";
            return RedirectToAction("Index");

            return View();
        }

    }
}
