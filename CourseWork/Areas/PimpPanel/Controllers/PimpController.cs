using CourseWork.Areas.PimpPanel.Models;
using CourseWork.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WhoreViewModel = CourseWork.Areas.PimpPanel.Models.WhoreViewModel;

namespace CourseWork.Areas.PimpPanel.Controllers
{
    public class PimpController : Controller
    {

        ApplicationDbContext _context;
        ApplicationUserManager userManager;

        public PimpController()
        {
            _context = new ApplicationDbContext();

            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));


        }
        // GET: PimpPanel/Pimp
        [Authorize(Roles ="Pimp")]
        public ActionResult Index()
        {
            
            var pimp = _context.Users.ToList().FirstOrDefault(x => x.Id == User.Identity.GetUserId());
            if (!(pimp.PimpConfirmed == true ? true:false))
            {
                return RedirectToAction("AccessDenied","Pimp");
            }

            var list = _context.Whores.Where(i=> i.PimpID == pimp.Id).Select(x => new WhoreViewModel() {
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

        [Authorize(Roles = "Pimp")]
        [HttpGet]
        public ActionResult CreateWhore()
        {

            return View();
        }

        [Authorize(Roles = "Pimp")]
        [HttpPost]
        public async Task<ActionResult> CreateWhore(WhoreViewModel model, HttpPostedFileBase PhotoFile)
        {
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                //var result = await userManager.CreateAsync(user);
                //if (result.Succeeded)
                //{

                   // var userID = _context.Users.ToList().FirstOrDefault(x => x.Email == model.Email).Id;

                    //_context.Whores.Add(new WhoreModel() {
                    //    PimpID = User.Identity.GetUserId(),
                    //    PricePerHour = model.PricePerHour,
                    //    UserID = userID
                    //});

                    byte[] imageData = null;

                    using (var binaryReader = new BinaryReader(PhotoFile.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(PhotoFile.ContentLength);
                    }
                    string imageUrl = Guid.NewGuid().ToString() + ".jpg";
                    using (Image image = Image.FromStream(new MemoryStream(imageData)))
                    {
                        image.Save("../../Images" + imageUrl, ImageFormat.Jpeg);
                    }
                    //_context.Images.Add(new ImageModel() {
                    //    ID = Guid.NewGuid().ToString(),
                    //    UserID = userID,
                    //    ImageName = imageUrl
                    //});

                    _context.SaveChanges();
                    return RedirectToAction("Index");

               // }
                return View(model);

               
            }
            return View(model);
        }


        public ActionResult AccessDenied()
        {

            return View();
        }
    }
}