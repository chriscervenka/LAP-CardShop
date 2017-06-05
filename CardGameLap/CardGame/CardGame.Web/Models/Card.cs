using System.ComponentModel.DataAnnotations;
using System;

namespace CardGame.Web.Models
{
    public class Card
    {
        [Required]
        [Display(Name = "KartenID")]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0,int.MaxValue)]
        public byte Mana { get; set; }

        [Range(0, int.MaxValue)]
        public short Attack { get; set; }

        [Range(0, int.MaxValue)]
        public short Life { get; set; }
        
        public string Type { get; set; }

        [Required]
        public byte[] Pic { get; set; }
    }
}