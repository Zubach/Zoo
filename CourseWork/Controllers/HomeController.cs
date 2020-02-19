using CourseWork.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseWork.Controllers
{
    public class HomeController : Controller
    {
            ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (User.IsInRole("Pimp"))
            {
                var userId = User.Identity.GetUserId();
                var pimp = _context.Users.FirstOrDefault(x => x.Id == userId);
                if (!(pimp.PimpConfirmed == true ? true : false))
                {
                    return RedirectToAction("AccessDenied", "Pimp" ,new {Area = "PimpPanel" });
                }
            }
            else
            {
              //  return RedirectToAction("Index", new { Area = "" });
            }


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}