using CourseWork.Models;
using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

[assembly: OwinStartupAttribute(typeof(CourseWork.Startup))]
namespace CourseWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {




            ConfigureAuth(app);

           

            Expression<Action> ex = ()=>CanRating();
            RecurringJob.AddOrUpdate(ex
                

                    , Cron.MinuteInterval(20)

                ) ;


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
}
