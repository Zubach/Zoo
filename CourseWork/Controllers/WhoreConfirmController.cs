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

        [HttpPost]
        public ActionResult Confirm()
        {

            return View();
        }
    }
}