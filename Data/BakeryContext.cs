using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Passionproject2.Data
{
    public class BakeryContext : DbContext
    {
        public BakeryContext() : base("name=BakeryContext")
        {
        }
        public System.Data.Entity.DbSet<Passionproject2.Models.Bakery> Bakeries { get; set; }

        public System.Data.Entity.DbSet<Passionproject2.Models.Customer> Customers { get; set; }
        public System.Data.Entity.DbSet<Passionproject2.Models.Order> Orders { get; set; }


    }
}
