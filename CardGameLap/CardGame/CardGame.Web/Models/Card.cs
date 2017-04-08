using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
        public int Mana { get; set; }

        [Range(0, int.MaxValue)]
        public int Attack { get; set; }

        [Range(0, int.MaxValue)]
        public int Life { get; set; }

        [Range(0, int.MaxValue)]
        public string Type { get; set; }

        public byte[] Pic { get; set; }
    }
}