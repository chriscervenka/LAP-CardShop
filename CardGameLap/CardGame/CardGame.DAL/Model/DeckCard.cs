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
    
    public partial class Deckcard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Deckcard()
        {
            this.AllCollections = new HashSet<Collection>();
        }
    
        public int ID { get; set; }
        public int ID_Deck { get; set; }
        public int ID_Card { get; set; }
    
        public virtual Card Card { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Collection> AllCollections { get; set; }
        public virtual Deck Deck { get; set; }
    }
}