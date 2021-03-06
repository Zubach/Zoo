﻿using CourseWork.Areas.AdminPanel.Models;
using CourseWork.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using WhoreConfirmViewModel = CourseWork.Areas.AdminPanel.Models.WhoreConfirmViewModel;

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
        
        public async Task<ActionResult> Confirm(string id)
        {
            var pimp = _context.Users.FirstOrDefault(x => x.Id == id);
            pimp.PimpConfirmed = true;

            var list = _context.WhoresConfirm.ToList().Where(x => x.PimpID == pimp.Id && x.Confirmed).ToList();
            foreach (var item in list) {
                var user = new ApplicationUser() { UserName = item.Email,Email = item.Email };

                var result = await userManager.CreateAsync(user, "Qwerty-1");
                if (result.Succeeded)
                {
                    var UserId = _context.Users.FirstOrDefault(x => x.Email == user.Email).Id;
                    userManager.AddToRole(UserId, "Whore");
                    _context.Whores.Add(new WhoreModel() {
                        PimpID = pimp.Id,
                        PricePerHour = 0,
                        UserID = UserId
                });
                }
                _context.WhoresConfirm.Remove(item);

            }
            
            

            

            _context.SaveChanges();
            
            return RedirectToAction("Index","Admin");
        }

        [Authorize(Roles = "Admin")]
        
        public ActionResult Cancel(string id)
        {
            var pimp = _context.Users.FirstOrDefault(x => x.Id == id);

            pimp.PimpConfirmed = false;

            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult StatusWhores(string id)
        {
            var list = _context.WhoresConfirm.ToList().Where(x => x.PimpID == id).Select(y=> new WhoreConfirmViewModel() {
                Confirmed = y.Confirmed,
                PimpID = y.PimpID,
                Email = y.Email
            });

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {

            return RedirectToAction("LogOff", "Home", new { area = "" });
        }
    }
}