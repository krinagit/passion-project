using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Passionproject2.Models
{
    public class Order
    {
        [Key]
       
        public int OrderID { get; set; }
    
        public string OrderItem { get; set; }
        //Order date is mentioned as MM/DD/YYYY formate ex:2/16/2020
        public string OrderDate { get; set; }

        public int OrderItemQty { get; set; }
        //established as the price of the whole appointment (13% HST tax included)
        
        //currency is CANADIAN (cad)
        public int OrderCost { get; set; }



        //Representing the Many in (One customer to many orders)        
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
    }
}