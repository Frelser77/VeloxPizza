using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace VeloxPizza.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Ordine> Ordine { get; set; }
        public virtual DbSet<Prodotto> Prodotto { get; set; }
        public virtual DbSet<ProdottoAcquistato> ProdottoAcquistato { get; set; }
        public virtual DbSet<Utente> Utente { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ordine>()
                .Property(e => e.Nota)
                .IsUnicode(false);

            modelBuilder.Entity<Ordine>()
                .HasMany(e => e.ProdottoAcquistato)
                .WithRequired(e => e.Ordine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotto>()
                .Property(e => e.Ingredienti)
                .IsUnicode(false);

            modelBuilder.Entity<Prodotto>()
                .HasMany(e => e.ProdottoAcquistato)
                .WithRequired(e => e.Prodotto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utente>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.Utente)
                .WillCascadeOnDelete(false);
        }
    }
}
