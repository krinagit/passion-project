using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Passionproject2.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string Emailid { get; set; }

        public string Phone { get; set; }

        public string CustomerAddress { get; set; }
    }
}