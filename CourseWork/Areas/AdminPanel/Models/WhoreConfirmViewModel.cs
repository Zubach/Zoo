using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseWork.Areas.AdminPanel.Models
{
    public class WhoreConfirmViewModel
    {
        public string PimpID { get; set; }

        public bool Confirmed { get; set; }

        public string Email { get; set; }
    }
}