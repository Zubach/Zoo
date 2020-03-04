using CourseWork.Areas.UserPanel.Models;
using CourseWork.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseWork.Areas.UserPanel.Controllers
{
    public class UserController : Controller
    {

        ApplicationDbContext _context;
        ApplicationUserManager userManager;

        public UserController()
        {
            _context = new ApplicationDbContext();

            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
        }
        // GET: UserPanel/User
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
             var list = _context.Orders.Where(x => x.UserID == userID).Select(y => new OrderViewModel() {
                 Confirmed = y.Confirmed,
                 UserID = y.UserID,
                 WhoreID = y.WhoreID,
                 MeetingTime = y.MeetingTime
             });

            return View(list);
        }
    }
}