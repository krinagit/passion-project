

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Passionproject2.Models.ViewModel
{
    public class ShowOrder
    {
        //information about a signle order
        public Order Order { get; set; }

        //information about multiple Customers
        public List<Customer> Customers { get; set; }
    }

  
}
