using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CourseWork.Models;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace CourseWork.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {


                case SignInStatus.Success:

                
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        private void SendMail(string email,string pimpID)
        {
            MailAddress from = new MailAddress("redheadzoo420@gmail.com", "RedHeadZoo");
           
            MailAddress to = new MailAddress(email);
           
            MailMessage m = new MailMessage(from, to);
           
            m.Subject = "Whore Confirm";
           
            m.Body = "<html><head> <meta charset='UTF-8'> <meta content='width=device-width, initial-scale=1' name='viewport'> <meta name='x-apple-disable-message-reformatting'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta content='telephone=no' name='format-detection'> <title></title> <style type='text/css'> a {text-decoration: none;} </style> </head><body><style>.rollover:hover .rollover-first { max-height: 0px !important; display: none !important;}.rollover:hover .rollover-second { max-height: none !important; display: block !important;}#outlook a { padding: 0;}.ExternalClass { width: 100%;}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div { line-height: 100%;}.es-button { mso-style-priority: 100 !important; text-decoration: none !important;}a[x-apple-data-detectors] { color: inherit !important; text-decoration: none !important; font-size: inherit !important; font-family: inherit !important; font-weight: inherit !important; line-height: inherit !important;}.es-desk-hidden { display: none; float: left; overflow: hidden; width: 0; max-height: 0; line-height: 0; mso-hide: all;}.es-button-border:hover { border-style: solid solid solid solid !important; background: #d6a700 !important; border-color: #42d159 #42d159 #42d159 #42d159 !important;}.es-button-border:hover a.es-button { background: #d6a700 !important; border-color: #d6a700 !important;}td .es-button-border:hover a.es-button-1582567107628 { background: #01d51d !important; border-color: #01d51d !important;}td .es-button-border-1582567107642:hover { background: #01d51d !important;}/*END OF IMPORTANT*/s { text-decoration: line-through;}html,body { width: 100%; font-family: roboto, 'helvetica neue', helvetica, arial, sans-serif; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;}table { mso-table-lspace: 0pt; mso-table-rspace: 0pt; border-collapse: collapse; border-spacing: 0px;}table td,html,body,.es-wrapper { padding: 0; Margin: 0;}.es-content,.es-header,.es-footer { table-layout: fixed !important; width: 100%;}img { display: block; border: 0; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic;}table tr { border-collapse: collapse;}p,hr { Margin: 0;}h1,h2,h3,h4,h5 { Margin: 0; line-height: 120%; mso-line-height-rule: exactly; font-family: tahoma, verdana, segoe, sans-serif;}p,ul li,ol li,a { -webkit-text-size-adjust: none; -ms-text-size-adjust: none; mso-line-height-rule: exactly;}.es-left { float: left;}.es-right { float: right;}.es-p5 { padding: 5px;}.es-p5t { padding-top: 5px;}.es-p5b { padding-bottom: 5px;}.es-p5l { padding-left: 5px;}.es-p5r { padding-right: 5px;}.es-p10 { padding: 10px;}.es-p10t { padding-top: 10px;}.es-p10b { padding-bottom: 10px;}.es-p10l { padding-left: 10px;}.es-p10r { padding-right: 10px;}.es-p15 { padding: 15px;}.es-p15t { padding-top: 15px;}.es-p15b { padding-bottom: 15px;}.es-p15l { padding-left: 15px;}.es-p15r { padding-right: 15px;}.es-p20 { padding: 20px;}.es-p20t { padding-top: 20px;}.es-p20b { padding-bottom: 20px;}.es-p20l { padding-left: 20px;}.es-p20r { padding-right: 20px;}.es-p25 { padding: 25px;}.es-p25t { padding-top: 25px;}.es-p25b { padding-bottom: 25px;}.es-p25l { padding-left: 25px;}.es-p25r { padding-right: 25px;}.es-p30 { padding: 30px;}.es-p30t { padding-top: 30px;}.es-p30b { padding-bottom: 30px;}.es-p30l { padding-left: 30px;}.es-p30r { padding-right: 30px;}.es-p35 { padding: 35px;}.es-p35t { padding-top: 35px;}.es-p35b { padding-bottom: 35px;}.es-p35l { padding-left: 35px;}.es-p35r { padding-right: 35px;}.es-p40 { padding: 40px;}.es-p40t { padding-top: 40px;}.es-p40b { padding-bottom: 40px;}.es-p40l { padding-left: 40px;}.es-p40r { padding-right: 40px;}.es-menu td { border: 0;}.es-menu td a img { display: inline !important;}a { font-family: roboto, 'helvetica neue', helvetica, arial, sans-serif; font-size: 16px; text-decoration: underline;}h1 { font-size: 30px; font-style: normal; font-weight: bold; color: #212121;}h1 a { font-size: 30px;}h2 { font-size: 24px; font-style: normal; font-weight: bold; color: #212121;}h2 a { font-size: 24px;}h3 { font-size: 20px; font-style: normal; font-weight: normal; color: #212121;}h3 a { font-size: 20px;}p,ul li,ol li { font-size: 16px; font-family: roboto, 'helvetica neue', helvetica, arial, sans-serif; line-height: 150%;}ul li,ol li { Margin-bottom: 15px;}.es-menu td a { text-decoration: none; display: block;}.es-wrapper { width: 100%; height: 100%; background-image: ; background-repeat: repeat; background-position: center top;}.es-wrapper-color { background-color: #f6f6f6;}.es-content-body { background-color: transparent;}.es-content-body p,.es-content-body ul li,.es-content-body ol li { color: #131313;}.es-content-body a { color: #2cb543;}.es-header { background-color: transparent; background-image: ; background-repeat: repeat; background-position: center top;}.es-header-body { background-color: #ffffff;}.es-header-body p,.es-header-body ul li,.es-header-body ol li { color: #333333; font-size: 14px;}.es-header-body a { color: #1376c8; font-size: 14px;}.es-footer { background-color: #f6f6f6; background-image: ; background-repeat: repeat; background-position: center top;}.es-footer-body { background-color: transparent;}.es-footer-body p,.es-footer-body ul li,.es-footer-body ol li { color: #131313; font-size: 16px;}.es-footer-body a { color: #ffffff; font-size: 16px;}.es-infoblock,.es-infoblock p,.es-infoblock ul li,.es-infoblock ol li { line-height: 120%; font-size: 12px; color: #ffffff;}.es-infoblock a { font-size: 12px; color: #ffffff;}a.es-button { border-style: solid; border-color: #ffc80a; border-width: 10px 20px 10px 20px; display: inline-block; background: #ffc80a; border-radius: 3px; font-size: 18px; font-family: roboto, 'helvetica neue', helvetica, arial, sans-serif; font-weight: normal; font-style: normal; line-height: 120%; color: #ffffff; text-decoration: none; width: auto; text-align: center;}.es-button-border { border-style: solid solid solid solid; border-color: #2cb543 #2cb543 #2cb543 #2cb543; background: #ffc80a; border-width: 0px 0px 0px 0px; display: inline-block; border-radius: 3px; width: auto;}@media only screen and (max-width: 600px) { .st-br { padding-left: 10px !important; padding-right: 10px !important; } p, ul li, ol li, a { font-size: 16px !important; line-height: 150% !important; } h1 { font-size: 30px !important; text-align: center; line-height: 120% !important; } h2 { font-size: 26px !important; text-align: center; line-height: 120% !important; } h3 { font-size: 20px !important; text-align: center; line-height: 120% !important; } h1 a { font-size: 30px !important; text-align: center; } h2 a { font-size: 26px !important; text-align: center; } h3 a { font-size: 20px !important; text-align: center; } .es-menu td a { font-size: 14px !important; } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size: 16px !important; } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size: 14px !important; } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size: 12px !important; } *[class='gmail-fix'] { display: none !important; } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align: center !important; } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align: right !important; } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align: left !important; } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display: inline !important; } .es-button-border { display: block !important; } a.es-button { font-size: 16px !important; display: block !important; border-left-width: 0px !important; border-right-width: 0px !important; } .es-btn-fw { border-width: 10px 0px !important; text-align: center !important; } .es-adaptive table, .es-btn-fw, .es-btn-fw-brdr, .es-left, .es-right { width: 100% !important; } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width: 100% !important; max-width: 600px !important; } .es-adapt-td { display: block !important; width: 100% !important; } .adapt-img { width: 100% !important; height: auto !important; } .es-m-p0 { padding: 0px !important; } .es-m-p0r { padding-right: 0px !important; } .es-m-p0l { padding-left: 0px !important; } .es-m-p0t { padding-top: 0px !important; } .es-m-p0b { padding-bottom: 0 !important; } .es-m-p20b { padding-bottom: 20px !important; } .es-mobile-hidden, .es-hidden { display: none !important; } .es-desk-hidden { display: table-row !important; width: auto !important; overflow: visible !important; float: none !important; max-height: inherit !important; line-height: inherit !important; } .es-desk-menu-hidden { display: table-cell !important; } table.es-table-not-adapt, .esd-block-html table { width: auto !important; } table.es-social { display: inline-block !important; } table.es-social td { display: inline-block !important; }}/* END RESPONSIVE STYLES */.es-p-default { padding-top: 20px; padding-right: 30px; padding-bottom: 0px; padding-left: 30px;}.es-p-all-default { padding: 0px;}</style> <div class='es-wrapper-color'> <table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0'> <tbody> <tr> <td class='esd-email-paddings st-br' valign='top'> <table cellpadding='0' cellspacing='0' class='es-header esd-header-popover' align='center'> <tbody> <tr> <td class='esd-stripe esd-checked' align='center' style='background-image:url(https://hpy.stripocdn.email/content/guids/CABINET_d21e6d1c5a6807d34e1eb6c59a588198/images/20841560930387653.jpg);background-color: transparent; background-position: center bottom; background-repeat: no-repeat;' bgcolor='transparent' background='https://hpy.stripocdn.email/content/guids/CABINET_d21e6d1c5a6807d34e1eb6c59a588198/images/20841560930387653.jpg'> <div> <table bgcolor='transparent' class='es-header-body' align='center' cellpadding='0' cellspacing='0' width='600' style='background-color: transparent;'> <tbody> <tr> <td class='esd-structure es-p20t es-p20r es-p20l' align='left'> <table cellpadding='0' cellspacing='0' width='100%'> <tbody> <tr> <td width='560' class='esd-container-frame' align='center' valign='top'> <table cellpadding='0' cellspacing='0' width='100%'> <tbody> <tr> <td align='center' class='esd-block-spacer' height='65'></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </div> </td> </tr> </tbody> </table> <table cellpadding='0' cellspacing='0' class='es-content' align='center'> <tbody> <tr> <td class='esd-stripe' align='center' bgcolor='transparent' style='background-color: transparent;'> <table bgcolor='transparent' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='600' style='background-color: transparent;'> <tbody> <tr> <td class='esd-structure es-p10t es-p10b es-p20r es-p20l' align='left' style='background-position: left bottom;'> <table cellpadding='0' cellspacing='0' width='100%'> <tbody> <tr> <td width='560' class='esd-container-frame' align='center' valign='top'> <table cellpadding='0' cellspacing='0' width='100%'> <tbody> <tr> <td align='center' class='esd-block-text'> <p style='font-size: 32px;'><strong>RedHeadZoo</strong></p> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> <tr> <td class='esd-structure es-p30t es-p15b es-p30r es-p30l' align='left' style='border-radius: 10px 10px 0px 0px; background-color: #ffffff; background-position: left bottom;' bgcolor='#ffffff'> <table cellpadding='0' cellspacing='0' width='100%'> <tbody> <tr> <td width='540' class='esd-container-frame' align='center' valign='top'> <table cellpadding='0' cellspacing='0' width='100%' style='background-position: left bottom;'> <tbody> <tr> <td align='center' class='esd-block-text'> <h1>Whore confirm</h1> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> <tr> <td class='esd-structure es-p5t es-p5b es-p30r es-p30l' align='left' style='border-radius: 0px 0px 10px 10px; background-position: left top; background-color: #ffffff;'> <table cellpadding='0' cellspacing='0' width='100%'> <tbody> <tr> <td width='540' class='esd-container-frame' align='center' valign='top'> <table cellpadding='0' cellspacing='0' width='100%'> <tbody> <tr> <td align='center' class='esd-block-button es-p10'><span class='es-button-border es-button-border-1582567107642' style='background: #0dfe2f;'><a href='"+ $"http://localhost:49444/WhoreConfirm/Confirm/?PimpID={pimpID}&EmailHash={email.GetHashCode().ToString()}" + "'" + "class='es-button es-button-1582567107628' target='_blank' style='background: #0dfe2f; border-color: #0dfe2f; padding: 10px; text-decoration:none; color:white; border-radius:2px; font-size:20pt'>Confirm</a></span></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> <table cellpadding='0' cellspacing='0' class='es-content esd-footer-popover' align='center'> <tbody> <tr> <td class='esd-stripe' align='center'> <table bgcolor='transparent' class='es-content-body' align='center' cellpadding='0' cellspacing='0' width='600'> <tbody> <tr> <td class='esd-structure' align='left' style='background-position: left top;'> <table cellpadding='0' cellspacing='0' width='100%'> <tbody> <tr> <td width='600' class='esd-container-frame' align='center' valign='top'> <table cellpadding='0' cellspacing='0' width='100%'> <tbody> <tr> <td align='center' class='esd-empty-container' style='display: none;'></td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </div></body></html>";

            m.IsBodyHtml = true;
           
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
           
            smtp.Credentials = new NetworkCredential("redheadzoo420@gmail.com", "Qwerty-1");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                var userID = UserManager.FindByEmail(user.Email).Id;
                if (result.Succeeded)
                {
                    if (model.Role == "User")
                    {
                        UserManager.AddToRole(userID, "User");

                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    }
                    else
                    {
                        user.PimpConfirmed = false;
                        UserManager.Update(user);

                        IEnumerable<string> emails =  model.Emails.Split(' ');

                        using (ApplicationDbContext _context = new ApplicationDbContext()) {

                            foreach (var item in emails) {
                                _context.WhoresConfirm.Add(new WhoreConfirmModel() {
                                    ID = Guid.NewGuid().ToString(),
                                    PimpID = userID,
                                    Confirmed = false,
                                    Email = item
                                });
                                SendMail(item, userID);
                            }


                            _context.SaveChanges();
                        }

                        UserManager.AddToRole(userID, "Pimp");
                        UserManager.AddToRole(userID, "User");
                    }
                    
                    
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}