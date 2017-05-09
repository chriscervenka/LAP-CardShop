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
    
    public partial class Card
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            this.AllDeckcards = new HashSet<Deckcard>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public byte Mana { get; set; }
        public short Life { get; set; }
        public short Attack { get; set; }
        public string Flavor { get; set; }
        public int ID_Type { get; set; }
        public Nullable<int> ID_Class { get; set; }
        public byte[] Pic { get; set; }
        public Nullable<int> ID_Collection { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Collection Collection { get; set; }
        public virtual Type Type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deckcard> AllDeckcards { get; set; }
    }
}
