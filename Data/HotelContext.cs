using Microsoft.EntityFrameworkCore;
using OtelRezervasyon.Models;

namespace OtelRezervasyon.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {
        }

        public DbSet<Oda> Odalar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Rezervasyon -> Musteri (1:1) ilişkisi
            modelBuilder.Entity<Rezervasyon>()
                .HasOne(r => r.Musteri)
                .WithMany()
                .HasForeignKey(r => r.MusteriId)
                .OnDelete(DeleteBehavior.Restrict);

            // Rezervasyon -> Oda (1:1) ilişkisi
            modelBuilder.Entity<Rezervasyon>()
                .HasOne(r => r.Oda)
                .WithMany()
                .HasForeignKey(r => r.OdaId)
                .OnDelete(DeleteBehavior.Restrict);

            // MusteriId için UNIQUE indeks
            modelBuilder.Entity<Rezervasyon>()
                .HasIndex(r => r.MusteriId)
                .IsUnique();

            // OdaId için UNIQUE indeks
            modelBuilder.Entity<Rezervasyon>()
                .HasIndex(r => r.OdaId)
                .IsUnique();
        }
    }
}
