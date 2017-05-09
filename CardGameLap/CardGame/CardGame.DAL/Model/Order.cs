namespace CardGame.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Collections = new HashSet<Collection>();
        }

        public int ID { get; set; }

        public DateTime? Orderdate { get; set; }

        public int? ID_Person { get; set; }

        public int? ID_Pack { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Collection> Collections { get; set; }

        public virtual Pack Pack { get; set; }

        public virtual Person Person { get; set; }
    }
}
