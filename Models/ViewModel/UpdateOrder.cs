using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Passionproject2.Models.ViewModel
{
    public class UpdateOrder
    {
        //when we need to update a pet
        //we need the pet info as well as a list of species

        public Order Order { get; set; }
        public List<Customer> Customers { get; set; }
    }
}