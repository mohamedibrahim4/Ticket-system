using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Platform_Task__Management_System.Models
{
    public class Facility
    {
        public int FacilityID { get; set; }
        [Required]
        public string FacilityName { get; set; }
        [Required]
        public string licenseNumber { get; set; }
        [Required]
        public string CodeFile { get; set; }
        [Required]
        public int ADT { get; set; }
        [Required]
        public int PPR { get; set; }
        [Required]
        public int OMP { get; set; }
        [Required]
        public int RAS { get; set; }
        //[Required]
        //public int TelephoneNumber { get; set; }


        [Required(ErrorMessage = "You must provide a phone number")]
        //[Display(Name = "TelephoneNumber")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string TelephoneNumber { get; set; }

        [Required]
        public string PasswordServer { get; set; }
        [Required]
        public string AnyDeskID { get; set; }
        [Required]
        public string AnyDeskPassword { get; set; }

        public bool IsSupport { get; set; }

        public string Comment { get; set; }

        //navigationProperty



        [ForeignKey("branch")]
        public int BranchID { get; set; }

        //[ScriptIgnore]
        public virtual Branch branch { get; set; }
        public virtual List<Task> Tasks { get; set; }

    }
}