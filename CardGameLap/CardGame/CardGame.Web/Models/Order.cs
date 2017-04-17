using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class Order
    {
        public Packages Pack { get; set; }

        public DateTime OrderDate { get; set; }

        public int PackQuantity { get; set; }

        public int UserBalance { get; set; }
    }
}