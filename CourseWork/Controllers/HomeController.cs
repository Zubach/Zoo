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
                    return RedirectToAction("AccessDenied", "Pimp", new { Area = "PimpPanel" });
                }
            }
            else
            {
                //  return RedirectToAction("Index", new { Area = "" });
            }

            var list = _context.Whores.ToList().Select(x =>
               new WhoreViewModel() {
                   PimpID = x.PimpID,
                   UserID = x.UserID,
                   PricePerHour = x.PricePerHour,
                   UserName = _context.Users.ToList().FirstOrDefault(y => y.Id == x.UserID).UserName,
                   ImageUrl = _context.Images.FirstOrDefault(j => j.UserID == x.UserID).ImageName,
                   Rating = x.Rating 


               });
            if (User.Identity.IsAuthenticated)
                return View(list);
            return RedirectToAction("Register", "Account");
        }

        public ActionResult SortedIndex(string filter)
        {

            if (User.IsInRole("Pimp"))
            {
                var userId = User.Identity.GetUserId();
                var pimp = _context.Users.FirstOrDefault(x => x.Id == userId);
                if (!(pimp.PimpConfirmed == true ? true : false))
                {
                    return RedirectToAction("AccessDenied", "Pimp", new { Area = "PimpPanel" });
                }
            }
            else
            {
                //  return RedirectToAction("Index", new { Area = "" });
            }

            var list = _context.Whores.ToList().Select(x =>
               new WhoreViewModel()
               {
                   PimpID = x.PimpID,
                   UserID = x.UserID,
                   PricePerHour = x.PricePerHour,
                   UserName = _context.Users.ToList().FirstOrDefault(y => y.Id == x.UserID).UserName,
                   ImageUrl = _context.Images.FirstOrDefault(j => j.UserID == x.UserID).ImageName,
                   Rating = x.Rating


               });

            List<WhoreViewModel> sortedList = null;

            switch (filter)
            {
                case "rating":
                    sortedList = (from e in list
                                  orderby e.Rating descending
                                  select e).ToList();
                    break;
                case "price_to_smaller":
                    sortedList = (from e in list
                                  orderby e.PricePerHour descending
                                  select e).ToList();
                    break;
                case "price_to_bigger":
                    sortedList = list.OrderBy(x => x.PricePerHour).ToList();
                    break;
            }

            if (User.Identity.IsAuthenticated)
                return View(sortedList);
            return RedirectToAction("Register", "Account");
        }


        [HttpGet]
        public ActionResult Order(string id)
        {

            var whore = _context.Whores.FirstOrDefault(x=> x.UserID == id);

            var model = new OrderWhoreViewModel() {
                UserID = User.Identity.GetUserId(),
                WhoreID = whore.UserID
                
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Order(OrderWhoreViewModel model)
        {
            var orders = _context.Orders.Where(x=> x.WhoreID == model.WhoreID).ToList();
            var date = new DateTime(model.MeetingTime_Date.Year,model.MeetingTime_Date.Month,model.MeetingTime_Date.Day,model.MeetingTime_Time.Hour,model.MeetingTime_Time.Minute,model.MeetingTime_Time.Second);
            if(orders.FirstOrDefault(x=>  x.MeetingTime ==  date) != null)
            {
                return View(model);
            }
            _context.Orders.Add(new OrderModel() {
                ID = Guid.NewGuid().ToString(),
                UserID = model.UserID,
                WhoreID = model.WhoreID,
                MeetingTime = date,
                Confirmed = false
            });
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
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