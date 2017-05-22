using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class PackOverviewModel
    {
        public int AmountMoney { get; set; }

        public List<Pack> CardPacks { get; set; }
    }
}