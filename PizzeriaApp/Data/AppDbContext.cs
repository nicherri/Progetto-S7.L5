using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ordine> Ordini { get; set; }
        public DbSet<Prodotto> Prodotti { get; set; }
        public DbSet<Ingrediente> Ingredienti { get; set; }
        public DbSet<ProdottoOrdinato> ProdottiOrdinati { get; set; }
        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Ruolo> Ruoli { get; set; }
        public DbSet<UtenteRuolo> UtentiRuoli { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ordine>()
                .HasOne(o => o.Utente)
                .WithMany()
                .HasForeignKey(o => o.UtenteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProdottoOrdinato>()
                .HasOne(po => po.Ordine)
                .WithMany(o => o.ProdottiOrdinati)
                .HasForeignKey(po => po.OrdineId);

            modelBuilder.Entity<ProdottoOrdinato>()
                .HasOne(po => po.Prodotto)
                .WithMany(p => p.ProdottiOrdinati)
                .HasForeignKey(po => po.ProdottoId);

            modelBuilder.Entity<UtenteRuolo>()
                .HasKey(ur => new { ur.UtenteId, ur.RuoloId });

            modelBuilder.Entity<UtenteRuolo>()
                .HasOne(ur => ur.Utente)
                .WithMany(u => u.UtentiRuoli)
                .HasForeignKey(ur => ur.UtenteId);

            modelBuilder.Entity<UtenteRuolo>()
                .HasOne(ur => ur.Ruolo)
                .WithMany(r => r.UtentiRuoli)
                .HasForeignKey(ur => ur.RuoloId);
        }
    }
}
