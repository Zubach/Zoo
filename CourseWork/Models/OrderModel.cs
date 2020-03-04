using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CourseWork.Models
{
    [Table("tblOrders")]
    public class OrderModel
    {
        [Key]
        public string ID { get; set; }


        [Required]
        public string UserID { get; set; }


        [Required]
        public string WhoreID { get; set; }

        [Required]

        public DateTime MeetingTime { get; set; }

        [Required]
        public bool Confirmed { get; set; }
    }
}