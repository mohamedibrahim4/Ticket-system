using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Platform_Task__Management_System.Models
{
    public class Software
    {
        public  int SoftwareID { get; set; }
        [Required]

        public string SoftwareName { get; set; }
        //navigationProperty
        public List<Task> tasks { get; set; }

    }
}