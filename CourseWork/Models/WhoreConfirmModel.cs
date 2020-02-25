using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CourseWork.Models
{

    [Table("tblWhoreCnf")]
    public class WhoreConfirmModel
    {

        [Key]
        public string ID { get; set; }

        public string PimpID { get; set; }

        
        [Required]

        public string Email { get; set; }

        [Required]
        public bool  Confirmed { get; set; }


    }
}