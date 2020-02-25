using CourseWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CourseWork.Controllers
{
    public class WhoreConfirmController : Controller
    {

        ApplicationDbContext _context;

        public WhoreConfirmController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpGet]
        public ActionResult Confirm(string emailHash,string pimpID)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            
            WhoreConfirmViewModel model = new WhoreConfirmViewModel()
            {
                PimpID = pimpID,
                EmailHash = emailHash,
                Confirmed = _context.WhoresConfirm.ToList().FirstOrDefault(x => x.PimpID == pimpID && x.Email.GetHashCode().ToString() == emailHash).Confirmed
            };

            return View(model);
        }

        
        public ActionResult Confirm(WhoreConfirmViewModel model)
        {
            var confirm = _context.WhoresConfirm.ToList().FirstOrDefault(x=> x.PimpID == model.PimpID && x.Email.GetHashCode().ToString() == model.EmailHash);
            confirm.Confirmed = true;
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}