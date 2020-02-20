using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Passionproject2.Models
{
    public class Bakery
    {
        [Key]
        public int BakeryID { get; set; }

        public string BakeryName { get; set; }

        public string BakeryAddress { get; set; }


    }
}
