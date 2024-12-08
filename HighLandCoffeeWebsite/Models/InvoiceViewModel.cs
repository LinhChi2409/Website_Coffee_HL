using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighLandCoffeeWebsite.Models
{
    public class InvoiceViewModel
    {
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}