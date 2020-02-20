using Passionproject2.Models;
using System.Collections.Generic;

namespace Passionproject2.Controllers
{
    internal class ShowOrder
    {
        internal Order Order;

        public List<Customer> Customer { get; internal set; }
    }
}