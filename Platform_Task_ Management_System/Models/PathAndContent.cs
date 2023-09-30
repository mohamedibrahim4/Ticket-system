using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Platform_Task__Management_System.Models
{
    public class PathAndContent
    {
        public int PathAndContentID { get; set; }
        public string FilePath { get; set; }

        public string ContentType { get; set; }
        //navigationProperty

        [ForeignKey("task")]
        public int TaskID { get; set; }
        public virtual Task task { get; set; }

    }
}