//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CardGame.DAL.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Collection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Collection()
        {
            this.AllCards = new HashSet<Card>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ID_Person { get; set; }
        public Nullable<int> ID_Order { get; set; }
        public Nullable<int> ID_Deckcard { get; set; }
        public Nullable<int> NumberOfCards { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Card> AllCards { get; set; }
        public virtual Deckcard Deckcard { get; set; }
        public virtual Order Order { get; set; }
        public virtual Person Person { get; set; }
    }
}
