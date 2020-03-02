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

            var list = _context.Orders.Where(x => x.WhoreID == User.Identity.GetUserId()).Select(y => new OrderViewModel() {
                MeetingTime = y.MeetingTime,
                UserID = y.UserID
            });

            return View(list);
        }
    }
}