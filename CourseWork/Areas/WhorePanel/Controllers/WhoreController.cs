using CourseWork.Areas.WhorePanel.Models;
using CourseWork.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseWork.Areas.WhorePanel.Controllers
{
    public class WhoreController : Controller
    {

        ApplicationDbContext _context;

        ApplicationUserManager userManager;

        public WhoreController()
        {
            _context = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));

        }


        // GET: WhorePanel/Whore
        [Authorize(Roles ="Whore")]
        public ActionResult Index()
        {

            var whoreID = User.Identity.GetUserId();

            var list = _context.Orders.Where(x => x.WhoreID == whoreID).Select(y => new OrderViewModel() {
                ID = y.ID,
                MeetingTime = y.MeetingTime,
                UserID = y.UserID,
                Confirmed = y.Confirmed
                
            });

            return View(list);
        }

        [Authorize(Roles ="Whore")]
        
        public ActionResult Confirm(string id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.ID == id);
            order.Confirmed = true;
            _context.SaveChanges();
            return RedirectToAction("Index", "Whore");
        }
    }
}