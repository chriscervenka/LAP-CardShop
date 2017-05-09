namespace CardGame.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Collection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Collection()
        {
            Cards = new HashSet<Card>();
        }

        public int ID { get; set; }

        public int? ID_Person { get; set; }

        public int? ID_Order { get; set; }

        public int? ID_Deckcard { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Card> Cards { get; set; }

        public virtual Deckcard Deckcard { get; set; }

        public virtual Order Order { get; set; }

        public virtual Person Person { get; set; }
    }
}
