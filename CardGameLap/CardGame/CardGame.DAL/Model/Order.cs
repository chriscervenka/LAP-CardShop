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
    
    public partial class Order
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> Orderdate { get; set; }
        public Nullable<int> ID_Person { get; set; }
        public Nullable<int> ID_Pack { get; set; }
        public int NumberOfPacks { get; set; }
    
        public virtual Pack Pack { get; set; }
        public virtual Person Person { get; set; }
    }
}
