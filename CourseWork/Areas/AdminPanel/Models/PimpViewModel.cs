using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseWork.Areas.AdminPanel.Models
{
    public class PimpViewModel
    {
        public string ID { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool? Confirmed { get; set; }
    }
}