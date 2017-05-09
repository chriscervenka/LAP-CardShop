namespace CardGame.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            Deckcards = new HashSet<Deckcard>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public byte Mana { get; set; }

        public short Life { get; set; }

        public short Attack { get; set; }

        [StringLength(500)]
        public string Flavor { get; set; }

        public int ID_Type { get; set; }

        public int? ID_Class { get; set; }

        public byte[] Pic { get; set; }

        public int? ID_Collection { get; set; }

        public virtual Class Class { get; set; }

        public virtual Collection Collection { get; set; }

        public virtual Type Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deckcard> Deckcards { get; set; }
    }
}
