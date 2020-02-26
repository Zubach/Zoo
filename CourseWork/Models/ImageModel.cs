using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CourseWork.Models
{
    [Table("tblImages")]
    public class ImageModel
    {
        [Key]
        public string ID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]

        public string ImageName { get; set; }
    }
}