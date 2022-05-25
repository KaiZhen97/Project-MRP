using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRP.Models
{
    public class DropdownItem
    {
        string _val;
        string _id;
        public string ID { get { return _id; } set { _id = value; } }
        public string Val { get { return _val; } set { _val = value; } }
    }
}