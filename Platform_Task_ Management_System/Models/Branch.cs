using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Platform_Task__Management_System.Models
{
    public class Branch
    {

        public int BranchID { get; set; }

        [Required]
        public string BranchName { get; set; }
        public virtual List<Facility> facilities { get; set; }

    }
}