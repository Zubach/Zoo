using CourseWork.Areas.AdminPanel.Controllers;
using CourseWork.Models;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

[assembly: OwinStartupAttribute(typeof(CourseWork.Startup))]
namespace CourseWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {




            ConfigureAuth(app);

            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            AdminController obj = new AdminController();
            app.UseHangfireDashboard("/myJobDashboard", new DashboardOptions()
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });



            Expression<Action> ex = () => CanRating();
            RecurringJob.AddOrUpdate(ex


                    , Cron.MinuteInterval(20)

                );


        }

       

        public void CanRating()
        {
            var _context = new ApplicationDbContext();
            var list =_context.Orders.Where(x => x.MeetingTime < DateTime.Now);
            foreach(var item in list)
            {
                item.CanRating = true;
            }
            _context.SaveChanges();
            // model.CanRating = true;


        }



        
      
    }
public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        if (HttpContext.Current.User.IsInRole("Admin"))
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
        else
        {
            return false;
        }
    }
}
}
