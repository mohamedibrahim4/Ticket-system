﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Platform_Task__Management_System.Models
{
    public class ProgrammerAddressType
    {
        //[Required]
        public string Country { get; set; }
        //[Required]
        public string City { get; set; }
        //[Required]
        public string Street { get; set; }
        //[Required]
        public string BuildingNumber { get; set; }
    }
}