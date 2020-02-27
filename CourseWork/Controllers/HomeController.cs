﻿using CourseWork.Models;
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

            var list = _context.Whores.ToList().Select( x =>
                new WhoreViewModel() {
                    PimpID = x.PimpID,
                    UserID = x.UserID,
                    PricePerHour = x.PricePerHour,
                    UserName = _context.Users.ToList().FirstOrDefault(y=>y.Id ==x.UserID ).UserName
                });
            if (User.Identity.IsAuthenticated) 
                return View(list);
            return RedirectToAction("Register","Account");
        }

        public ActionResult Order(string id)
        {

            var whore = _context.Whores.FirstOrDefault(x=> x.UserID == id);

            var model = new OrderWhoreViewModel() {
                UserID = User.Identity.GetUserId(),
                WhoreID = whore.UserID
            };

            return View(model);
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