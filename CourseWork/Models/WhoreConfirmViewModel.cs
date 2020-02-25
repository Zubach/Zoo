using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseWork.Models
{
    public class WhoreConfirmViewModel
    {
        public string PimpID { get; set; }

        public string EmailHash { get; set; }

        public bool Confirmed { get; set; }
    }
}