using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CourseWork.Models
{
    [Table("tblWhores")]
    public class WhoreModel
    {
        [Key]
        public string UserID { get; set; }

        [Required]
        public decimal PricePerHour { get; set; }


        [Required]
        public string PimpID { get; set; }


        
        public float? Rating { get; set; }



    }
}