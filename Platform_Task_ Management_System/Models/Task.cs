using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Platform_Task__Management_System.Models
{
    public class Task
    {
        public int TaskID { get; set; }
        [Required]

        public string TaskName { get; set; }
        [Required]

        [MaxLength(4000)]
        public string TaskRequirements { get; set; }
        [MaxLength(4000)]
        public string Notes { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yy}")]

        public DateTime? DeadLineDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yy}")]

        public DateTime? CreationDate { get; set; }
        public IEnumerable<HttpPostedFileBase> files { get; set; }
        public string FilePath { get; set; }

        public string ContentType { get; set; }
        //navigationProperty

        //public List<PathAndContent> pathAndContents { get; set; }


        //[ForeignKey("fileModel")]
        //public int FileModelID { get; set; }
        //public FileModel fileModel { get; set; }

        [ForeignKey("facility")]
        public int FacilityID { get; set; }
        public Facility facility { get; set; }


        //Id current user
        [ForeignKey("applicationUser")]
        public string applicationUserID{ get; set; }
        public ApplicationUser applicationUser { get; set; }
        //Id Status
        public Status status { get; set; }
        //Id Software 
        [ForeignKey("software")]
        public int SoftwareID { get; set; }
        public Software software { get; set; }
       








    }
}