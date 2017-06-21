using System;

namespace CardGame.Web.Models
{
    public class Statistic
    {
        public int NumCards { get; set; }
        public int NumUsers { get; set; }
        public int NumDecks { get; set; }

        /// <summary>
        /// LAP
        /// </summary>
        public int NumPacks { get; set; }
        public Order Order { get; set; }
        public DateTime CreationTime { get; set; }

        public Statistic()
        {
            CreationTime = DateTime.Now;
        }

    }

}