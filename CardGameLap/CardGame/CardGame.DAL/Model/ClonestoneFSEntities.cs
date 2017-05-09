namespace CardGame.DAL.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ClonestoneFSEntities : DbContext
    {
        public ClonestoneFSEntities()
            : base("name=ClonestoneFSConfig")
        {
        }

        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Deckcard> Deckcards { get; set; }
        public virtual DbSet<Deck> Decks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Pack> Packs { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<vEditor> vEditors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Card>()
                .Property(e => e.Flavor)
                .IsUnicode(false);

            modelBuilder.Entity<Card>()
                .HasMany(e => e.Deckcards)
                .WithRequired(e => e.Card)
                .HasForeignKey(e => e.ID_Card)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .Property(e => e.Class1)
                .IsUnicode(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Cards)
                .WithOptional(e => e.Class)
                .HasForeignKey(e => e.ID_Class);

            modelBuilder.Entity<Collection>()
                .HasMany(e => e.Cards)
                .WithOptional(e => e.Collection)
                .HasForeignKey(e => e.ID_Collection);

            modelBuilder.Entity<Deckcard>()
                .HasMany(e => e.Collections)
                .WithOptional(e => e.Deckcard)
                .HasForeignKey(e => e.ID_Deckcard);

            modelBuilder.Entity<Deck>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Deck>()
                .HasMany(e => e.Deckcards)
                .WithRequired(e => e.Deck)
                .HasForeignKey(e => e.ID_Deck)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Collections)
                .WithOptional(e => e.Order)
                .HasForeignKey(e => e.ID_Order);

            modelBuilder.Entity<Pack>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Pack>()
                .Property(e => e.Packprice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Pack>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Pack)
                .HasForeignKey(e => e.ID_Pack);

            modelBuilder.Entity<Person>()
                .Property(e => e.Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Gamertag)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Password)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Salt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Collections)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.ID_Person);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Decks)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.ID_Person);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Person)
                .HasForeignKey(e => e.ID_Person);

            modelBuilder.Entity<Type>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .HasMany(e => e.Cards)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.ID_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<vEditor>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<vEditor>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<vEditor>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<vEditor>()
                .Property(e => e.lastname)
                .IsUnicode(false);
        }
    }
}
