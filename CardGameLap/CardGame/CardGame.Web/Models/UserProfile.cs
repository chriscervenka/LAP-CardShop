using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Models
{
    public class UserProfile
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
     
        public string Email { get; set; }
        public decimal Currency { get; set; }

        public int NumDistinctCardsOwned { get; set; }
        public int NumTotalCardsOwned { get; set; }
        public int NumDecksOwned { get; set; }
    }
}