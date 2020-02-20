using CourseWork.Areas.AdminPanel.Models;
using CourseWork.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseWork.Areas.AdminPanel.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext _context;

        ApplicationUserManager userManager;

        public AdminController()
        {
            _context = new ApplicationDbContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
        }

        // GET: AdminPanel/Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            List<PimpViewModel> pimps = new List<PimpViewModel>();

            foreach (var user in users)
            {
                
                if (userManager.IsInRole(user.Id,"Pimp"))
                {
                    pimps.Add(
                        new PimpViewModel() {
                            ID = user.Id,
                            Confirmed = user.PimpConfirmed,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            UserName = user.UserName
                        });

                }
            }

            var users_m = users.Select(x => new UserViewModel() {
                Email = x.Email,
                ID = x.Id,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName
            }).ToList();


            users_m.RemoveAll( x => userManager.IsInRole(x.ID,"Pimp") || userManager.IsInRole(x.ID, "Admin"));

            dynamic model = new ExpandoObject();

            model.Pimps = pimps;
            model.Users = users_m;

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Confirm(string id)
        {
            var pimp = _context.Users.FirstOrDefault(x => x.Id == id);
            pimp.PimpConfirmed = true;
            _context.SaveChanges();
            return Index();
        }

    }
}