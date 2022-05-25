using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFrameWorkLib.Models
{
    public class User
    {
        public string AccessId { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Designation { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string PhoneNum { get; set; }
        public string StaffNumber { get; set; }
    }
}