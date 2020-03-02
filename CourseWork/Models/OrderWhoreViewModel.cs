using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseWork.Models
{
    public class OrderWhoreViewModel
    {
        [DataType(DataType.Date)]
        [Required]
        public DateTime MeetingTime_Date { get; set; }

        [DataType(DataType.Date | DataType.DateTime)]
        [Required]
        public DateTime MeetingTime_Time { get; set; }

        public string UserID { get; set; }

        public string WhoreID { get; set; }

    }
}