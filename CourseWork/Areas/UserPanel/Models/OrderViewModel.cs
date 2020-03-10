using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseWork.Areas.UserPanel.Models
{
    public class OrderViewModel
    {
        public string UserID { get; set; }


        
        public string WhoreID { get; set; }

        public string WhoreName { get; set; }



        public DateTime MeetingTime { get; set; }


        public bool Confirmed { get; set; }


        public bool CanRating { get; set; }
    }
}