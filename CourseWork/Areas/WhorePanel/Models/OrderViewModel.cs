using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseWork.Areas.WhorePanel.Models
{
    public class OrderViewModel
    {
        public string ID { get; set; }
        public DateTime MeetingTime { get; set; }

        public string UserID { get; set; }

        public bool Confirmed { get; set; }

    }
}