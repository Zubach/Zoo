using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseWork.Models
{
    public class OrderWhoreViewModel
    {
        [DataType(DataType.DateTime)]

        public DateTime MeetingTime { get; set; }

        public string UserID { get; set; }

        public string WhoreID { get; set; }

    }
}