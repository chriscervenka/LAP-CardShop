﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ClonestoneFSEntities : DbContext
    {
        public ClonestoneFSEntities()
            : base("name=ClonestoneFSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Card> AllCards { get; set; }
        public virtual DbSet<Class> AllClasses { get; set; }
        public virtual DbSet<Collection> AllCollections { get; set; }
        public virtual DbSet<Deck> AllDecks { get; set; }
        public virtual DbSet<DeckCard> AllDeckCards { get; set; }
        public virtual DbSet<Order> AllOrders { get; set; }
        public virtual DbSet<Pack> AllPacks { get; set; }
        public virtual DbSet<Person> AllPersons { get; set; }
        public virtual DbSet<Type> AllTypes { get; set; }
    }
}
