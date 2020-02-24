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
            WhoreConfirmViewModel model = new WhoreConfirmViewModel()
            {
                PimpID = pimpID,
                EmailHash = emailHash
            };
            return View(model);
        }

        
        public ActionResult Confirm(WhoreConfirmViewModel model)
        {
            var confirm = _context.WhoresConfirm.FirstOrDefault(x=> x.PimpID == model.PimpID && x.Email.GetHashCode().ToString() == model.EmailHash);
            confirm.Confirmed = true;
            _context.SaveChanges();
            return View();
        }
    }
}