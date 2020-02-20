using CourseWork.Areas.PimpPanel.Models;
using CourseWork.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseWork.Areas.PimpPanel.Controllers
{
    public class PimpController : Controller
    {

        ApplicationDbContext _context;
        public PimpController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: PimpPanel/Pimp
        [Authorize(Roles ="Pimp")]
        public ActionResult Index()
        {
            
            var pimp = _context.Users.FirstOrDefault(x => x.Id == User.Identity.GetUserId());
            if (!(pimp.PimpConfirmed == true ? true:false))
            {
                return AccessDenied();
            }

            var list = _context.Whores.Select(x => new WhoreViewModel() {
                PimpID = x.PimpID,
                UserID = x.UserID,
                PricePerHour = x.PricePerHour
            });
            var users = _context.Users.ToList();
            foreach(var item in list)
            {
                item.Email = users.FirstOrDefault(x => x.Id == item.UserID).Email;
            }
            
            return View(list);

        }

        public ActionResult AccessDenied()
        {

            return View();
        }
    }
}